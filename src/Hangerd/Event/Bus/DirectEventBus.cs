using System.Collections.Generic;
using Hangerd.Uow;

namespace Hangerd.Event.Bus
{
	public class DirectEventBus : UnitOfWorkBase, IEventBus
	{
		private readonly IEventDispatcher _eventDispatcher;
		private readonly object _queueLock = new object();
		private readonly Queue<dynamic> _eventQueue = new Queue<dynamic>();

		public DirectEventBus(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		public void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
		{
			lock (_queueLock)
			{
				_eventQueue.Enqueue(@event);
			}
		}

		protected override void CommitUow()
		{
			lock (_queueLock)
			{
				while (_eventQueue.Count > 0)
					_eventDispatcher.DispatchEvent(_eventQueue.Dequeue());
			}
		}

		protected override void InternalDispose()
		{
			lock (_queueLock)
			{
				_eventQueue.Clear();
			}

			base.InternalDispose();
		}
	}
}
