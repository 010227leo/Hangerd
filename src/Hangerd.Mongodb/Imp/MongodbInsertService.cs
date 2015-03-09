using Hangerd.Components;
using Hangerd.MemoryQueue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangerd.Mongodb.Imp
{
	public class MongodbInsertService : MongodbServiceBase, IMongodbInsertService
	{
		private readonly Dictionary<string, IMemoryQueueService<MongodbItem>> _submitDataMemoryQueueServices = new Dictionary<string, IMemoryQueueService<MongodbItem>>();

		public void Insert(MongodbItem item)
		{
			try
			{
				var databaseName = item.DatabaseName;

				if (!_submitDataMemoryQueueServices.ContainsKey(databaseName))
				{
					lock (_submitDataMemoryQueueServices)
					{
						if (!_submitDataMemoryQueueServices.ContainsKey(databaseName))
						{
							var memoryQueueService = LocalServiceLocator.GetService<IMemoryQueueService<MongodbItem>>();

							memoryQueueService.Init(new MemoryQueueServiceConfiguration<MongodbItem>(string.Format("MongodbInsertServiceQueue<{0}>", databaseName), InternalSubmitData)
							{
								ConsumeIntervalMilliseconds = 1000,
								ConsumeItemCountInOneBatch = 100,
								ConsumeThreadCount = 1
							});

							_submitDataMemoryQueueServices.Add(databaseName, memoryQueueService);
						}
					}
				}

				if (_submitDataMemoryQueueServices.ContainsKey(databaseName))
					_submitDataMemoryQueueServices[databaseName].Enqueue(item);
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception("MongodbInsertService.Insert error: {0}", ex.Message);
			}
		}

		private void InternalSubmitData(IList<MongodbItem> items)
		{
			if (items == null || items.Count == 0)
				return;

			var databaseName = items.First().DatabaseName;

			try
			{
				if (CachedMongoServer == null) 
					return;

				var database = CachedMongoServer.GetDatabase(databaseName);

				foreach (var item in items)
				{
					var collection = database.GetCollection(item.TableName);

					collection.Insert(item.Data);
				}
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception("MongodbInsertService.InternalSubmitData<{0}> error: {1}", databaseName, ex.Message);
			}
		}

		protected override void InternalDispose()
		{
			foreach (var queue in _submitDataMemoryQueueServices.Values)
				queue.Dispose();

			base.InternalDispose();
		}
	}
}
