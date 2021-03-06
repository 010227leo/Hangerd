﻿using System.Collections.Generic;
using Hangerd.Components;
using Hangerd.Event;
using Hangerd.Event.Bus;
using Hangerd.Uow;
using NUnit.Framework;

namespace Hangerd.Test.Event.Bus
{
	public class EventBusTest : TestBase
	{
		public static List<string> EventHandledResults { get; private set; }

		[Test]
		public void PublishAndCommitTest()
		{
			var dispatcher = LocalServiceLocator.GetService<IEventDispatcher>();

			if (dispatcher == null)
				Assert.Fail("EventDispatcher is null");

			//dispatcher.AutoRegister();

			dispatcher.Register<TestEvent, TestEventHandler1>();
			dispatcher.Register<TestEvent, TestEventHandler2>();

			using (var bus = UnitOfWorkManager.Begin<IEventBus>())
			{
				EventHandledResults = new List<string>();

				bus.Publish(new TestEvent());
				bus.Commit();

				Assert.AreEqual(2, EventHandledResults.Count);
			}
		}
	}

	public class TestEventHandler1 : IEventHandler<TestEvent>
	{
		public void Handle(TestEvent @event)
		{
			EventBusTest.EventHandledResults.Add("TestEvent handled by TestEventHandler1");
		}
	}

	public class TestEventHandler2 : IEventHandler<TestEvent>
	{
		public void Handle(TestEvent @event)
		{
			EventBusTest.EventHandledResults.Add("TestEvent handled by TestEventHandler2");
		}
	}
}
