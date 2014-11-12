namespace Hangerd.Events
{
	using Hangerd.Components;
	using Hangerd.Entity;

	/// <summary>
	/// 表示继承于该类的类型为领域事件。
	/// </summary>
	public abstract class DomainEvent : IDomainEvent
	{
		private readonly EntityBase _source;

		public DomainEvent() { }

		public DomainEvent(EntityBase source)
		{
			this._source = source;
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
			{
				//todo: async

				handler.Handle(domainEvent);
			}
		}

		#endregion
	}
}
