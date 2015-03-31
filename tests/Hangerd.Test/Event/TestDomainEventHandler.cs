using Hangerd.Event;
using Hangerd.Test.Entity;

namespace Hangerd.Test.Event
{
	public class TestDomainEventHandler : IDomainEventHandler<TestEvent>
	{
		public void Handle(TestEvent @event)
		{
			var sampleEntity = @event.Source as SampleEntity;

			if (sampleEntity != null)
				sampleEntity.Order++;
		}
	}
}
