namespace Hangerd.Test.Event.Bus
{
	using Hangerd.Components;
	using Hangerd.Event.Bus;
	using NUnit.Framework;
	using System;
	using System.Collections.Generic;

	public class DefaultEventBusTest : TestBase
	{
		private static readonly List<string> _eventHandledResults = new List<string>();

		public static List<string> EventHandledResults
		{
			get
			{
				return _eventHandledResults;
			}
		}

		[Test]
		public void PublishAndCommitTest()
		{
			var bus = LocalServiceLocator.GetService<IEventBus>();

			if (bus == null)
			{
				Assert.Fail("EventBus is null");
			}

			_eventHandledResults.Clear();

			bus.Publish(new TestEvent());
			bus.Commit();

			Assert.AreEqual(2, _eventHandledResults.Count);
		}
	}
}
