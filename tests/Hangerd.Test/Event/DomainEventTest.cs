using Hangerd.Event;
using Hangerd.Extensions;
using NUnit.Framework;

namespace Hangerd.Test.Event
{
	public class DomainEventTest : TestBase
	{
		[Test]
		public void PublishTest()
		{
			HangerdFramework.Container.RegisterMultipleTypesAsPerResolve<IDomainEventHandler<TestEvent>, TestDomainEventHandler>();

			var sampleEnity = new SampleEntity
			{
				Order = 1
			};

			DomainEvent.Publish(new TestEvent(sampleEnity));

			Assert.AreEqual(2, sampleEnity.Order);
		}
	}
}
