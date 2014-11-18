namespace Hangerd.Test.MemoryQueue
{
	using Hangerd.Components;
	using Hangerd.MemoryQueue;
	using NUnit.Framework;
	using System.Threading;

	public class MemoryQueueServiceTest : TestBase
	{
		[Test]
		public void OneQueueTest()
		{
			var testQueueService = LocalServiceLocator.GetService<IMemoryQueueService<string>>();

			testQueueService.Init(new MemoryQueueServiceConfiguration<string>("test_queue", items => { }));

			var itemCount = 10;

			for (var i = 0; i < itemCount; i++)
			{
				testQueueService.Enqueue(string.Format("test_{0}", i));
			}

			Thread.Sleep(1000);

			Assert.AreEqual(itemCount, testQueueService.TotalConsumeItemCount);
		}

		[Test]
		public void MultiQueuesTest()
		{
			var testQueueService1 = LocalServiceLocator.GetService<IMemoryQueueService<string>>();
			var testQueueService2 = LocalServiceLocator.GetService<IMemoryQueueService<int>>();

			testQueueService1.Init(new MemoryQueueServiceConfiguration<string>("test_queue_1", items => { }));
			testQueueService2.Init(new MemoryQueueServiceConfiguration<int>("test_queue_2", items => { }));

			var itemCount1 = 10;
			var itemCount2 = 20;

			for (var i = 0; i < itemCount1; i++)
			{
				testQueueService1.Enqueue(string.Format("test_{0}", i));
			}

			for (var i = 0; i < itemCount2; i++)
			{
				testQueueService2.Enqueue(i);
			}

			Thread.Sleep(1000);

			Assert.AreEqual(itemCount1, testQueueService1.TotalConsumeItemCount);
			Assert.AreEqual(itemCount2, testQueueService2.TotalConsumeItemCount);
		}
	}
}
