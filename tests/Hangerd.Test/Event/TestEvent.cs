
namespace Hangerd.Test.Event
{
	using Hangerd.Entity;
	using Hangerd.Event;

	public class TestEvent : DomainEvent
	{
		public TestEvent() { }

		public TestEvent(EntityBase sampleEntity)
			: base(sampleEntity)
		{ }
	}
}
