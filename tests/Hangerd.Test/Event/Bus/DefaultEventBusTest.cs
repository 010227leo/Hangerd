namespace Hangerd.Test.Event.Bus
{
	using Hangerd.Components;
	using Hangerd.Event.Bus;
	using NUnit.Framework;
	using System;
	using System.Collections.Generic;

	public class DefaultEventBusTest : TestBase
	{
		public static List<string> EventHandledResults { get; private set; }

		[Test]
		public void PublishAndCommitTest()
		{
			var bus = LocalServiceLocator.GetService<IEventBus>();

			if (bus == null)
			{
				Assert.Fail("EventBus is null");
			}

			EventHandledResults = new List<string>();

			bus.Publish(new TestEvent());
			bus.Commit();

			Assert.AreEqual(2, EventHandledResults.Count);
		}
	}
}
