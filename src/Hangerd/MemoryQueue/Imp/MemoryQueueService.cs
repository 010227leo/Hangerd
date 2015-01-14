namespace Hangerd.MemoryQueue.Imp
{
	using Hangerd.Components;
	using Hangerd.Extensions;
	using System;
	using System.Collections.Generic;
	using System.Threading;

	public class MemoryQueueService<T> : Disposable, IMemoryQueueService<T>
	{
		private readonly Queue<T> _memoryQueue = new Queue<T>();
		private readonly List<Thread> _consumeThreads = new List<Thread>();
		private MemoryQueueServiceConfiguration<T> _configuration;
		private long _totalConsumeItemCount;

		public long TotalConsumeItemCount
		{
			get { return _totalConsumeItemCount; }
		}

		public void Init(MemoryQueueServiceConfiguration<T> configuration)
		{
			_configuration = configuration;

			InternalInit();

			LocalLoggingService.Info("Init MemoryQueue '{0}'", configuration.MemoryQueueName);
		}

		public void Enqueue(T item)
		{
			if (_configuration == null)
			{
				throw new Exception(string.Format("MemoryQueue uninitialized: '{0}'", GetType().FullName));
			}

			try
			{
				lock (_memoryQueue)
				{
					_memoryQueue.Enqueue(item);
				}
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception(ex);
			}
		}

		public void EnqueueBatch(IList<T> item)
		{
			item.Each(Enqueue);
		}

		private void InternalInit()
		{
			lock (_memoryQueue)
			{
				if (_consumeThreads.Count == 0)
				{
					for (var i = 0; i < _configuration.ConsumeThreadCount; i++)
					{
						var thread = new Thread(Consume)
						{
							Name = string.Format("{0}_{1}_{2}", "Hanger.MemoryQueue", _configuration.MemoryQueueName, i),
							IsBackground = true,
						};

						thread.Start();

						_consumeThreads.Add(thread);
					}
				}
			}
		}

		private void Consume()
		{
			while (true)
			{
				var sleepTime = _configuration.ConsumeIntervalMilliseconds;
				var consumeItems = new List<T>();

				try
				{
					lock (_memoryQueue)
					{
						var consumeCount = Math.Min(_memoryQueue.Count, _configuration.ConsumeItemCountInOneBatch);

						for (var i = 0; i < consumeCount; i++)
						{
							consumeItems.Add(_memoryQueue.Dequeue());
						}
					}

					if (consumeItems.Count > 0)
					{
						_configuration.ConsumeAction(consumeItems);

						Interlocked.Add(ref _totalConsumeItemCount, consumeItems.Count);
					}
				}
				catch (Exception ex)
				{
					LocalLoggingService.Exception("MemoryQueueService('{0}') consume error: {1}", _configuration.MemoryQueueName, ex.Message);
				}
				finally
				{
					Thread.Sleep(sleepTime);
				}
			}
		}
	}
}
