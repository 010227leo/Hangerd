namespace Hangerd.Mongodb.Test
{
	using Hangerd.Components;
	using Hangerd.Test;
	using NUnit.Framework;
	using System;
	using System.Threading;

	public class MongodbInsertServiceTest : TestBase
	{
		[Test]
		public void BatchInsertTest()
		{
			var mongodbInsertService = LocalServiceLocator.GetService<IMongodbInsertService>();

			for (var i = 0; i < 10; i++)
			{
				var item1 = new MongodbItem
				{
					DatabaseName = "testdb1",
					TableName = "OperationLog",
					Data = new TestData
					{
						Operator = string.Format("010227-{0}", i),
						OperationType = "login",
						IsSuccess = true,
						OperationTime = DateTime.Now
					}
				};

				mongodbInsertService.Insert(item1);

				var item2 = new MongodbItem
				{
					DatabaseName = "testdb2",
					TableName = "OperationLog",
					Data = new TestData
					{
						Operator = string.Format("leo-{0}", i),
						OperationType = "change_password",
						IsSuccess = true,
						OperationTime = DateTime.Now
					}
				};

				mongodbInsertService.Insert(item2);
			}

			Thread.Sleep(2000);
		}
	}
}
