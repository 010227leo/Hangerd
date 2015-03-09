using Hangerd.Entity;
using Hangerd.Event;

namespace Hangerd.Test.Event
{
	public class TestEvent : DomainEvent
	{
		public TestEvent() { }

		public TestEvent(EntityBase sampleEntity)
			: base(sampleEntity)
		{ }
	}
}
