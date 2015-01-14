namespace Hangerd.Test.Event
{
	using Hangerd.Event;
	using Hangerd.Test.Event.Bus;

	public class TestEventHandler1 : IEventHandler<TestEvent>
	{
		public void Handle(TestEvent @event)
		{
			DefaultEventBusTest.EventHandledResults.Add("TestEvent handled by TestEventHandler1");
		}
	}
}
