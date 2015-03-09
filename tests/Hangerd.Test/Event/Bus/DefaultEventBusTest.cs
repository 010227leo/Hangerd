using Hangerd.Components;
using Hangerd.Event.Bus;
using NUnit.Framework;
using System.Collections.Generic;

namespace Hangerd.Test.Event.Bus
{
	public class DefaultEventBusTest : TestBase
	{
		public static List<string> EventHandledResults { get; private set; }

		[Test]
		public void PublishAndCommitTest()
		{
			var bus = LocalServiceLocator.GetService<IEventBus>();

			if (bus == null)
				Assert.Fail("EventBus is null");

			EventHandledResults = new List<string>();

			bus.Publish(new TestEvent());
			bus.Commit();

			Assert.AreEqual(2, EventHandledResults.Count);
		}
	}
}
