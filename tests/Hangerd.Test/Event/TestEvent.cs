namespace Hangerd.Test.Event
{
	using Hangerd.Event;

	public class TestEvent : DomainEvent
	{
		public TestEvent() { }

		public TestEvent(SampleEntity sampleEntity)
			: base(sampleEntity)
		{ }
	}
}
