using System;
using Hangerd.Components;
using Hangerd.Entity;

namespace Hangerd.Event
{
	public abstract class DomainEvent : IDomainEvent
	{
		private readonly EntityBase _source;

		public Guid Id { get; set; }

		public DateTime Timestamp { get; set; }

		protected DomainEvent() { }

		protected DomainEvent(EntityBase source)
		{
			_source = source;

			Id = Guid.NewGuid();
			Timestamp = DateTime.Now;
		}

		public EntityBase Source
		{
			get { return _source; }
		}

		#region Public Static Methods

		public static void Publish<TDomainEvent>(TDomainEvent domainEvent)
			where TDomainEvent : class, IDomainEvent
		{
			var handlers = LocalServiceLocator.GetServices<IDomainEventHandler<TDomainEvent>>();

			foreach (var handler in handlers)
				handler.Handle(domainEvent);
		}

		#endregion
	}
}
