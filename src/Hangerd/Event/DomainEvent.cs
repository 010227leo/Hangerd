using System;
using Hangerd.Components;
using Hangerd.Domain.Entity;
using Hangerd.Event.Bus;
using Hangerd.Uow;

namespace Hangerd.Event
{
	public abstract class DomainEvent : IDomainEvent
	{
		private readonly EntityBase _source;

		public EntityBase Source
		{
			get { return _source; }
		}

		public Guid Id { get; private set; }

		public DateTime Timestamp { get; private set; }

		protected DomainEvent() { }

		protected DomainEvent(EntityBase source)
		{
			_source = source;

			Id = Guid.NewGuid();
			Timestamp = DateTime.Now;
		}

		#region Public Static Methods

		public static void Publish<TDomainEvent>(TDomainEvent domainEvent)
			where TDomainEvent : class, IDomainEvent
		{
			var handlers = LocalServiceLocator.GetServices<IDomainEventHandler<TDomainEvent>>();

			foreach (var handler in handlers)
				handler.Handle(domainEvent);

			var uowProvider = LocalServiceLocator.GetService<ICurrentUowProvider>();
			var eventBus = uowProvider.GetCurrent<IEventBus>();

			if (eventBus != null)
			{
				eventBus.Publish(domainEvent);
			}
		}

		#endregion
	}
}
