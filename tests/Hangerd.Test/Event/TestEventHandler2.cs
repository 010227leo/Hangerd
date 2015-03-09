using Hangerd.Event;
using Hangerd.Test.Event.Bus;

namespace Hangerd.Test.Event
{
	public class TestEventHandler2 : IEventHandler<TestEvent>
	{
		public void Handle(TestEvent @event)
		{
			DefaultEventBusTest.EventHandledResults.Add("TestEvent handled by TestEventHandler2");
		}
	}
}
