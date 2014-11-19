namespace Hangerd.Event.Bus
{
	using Hangerd.Utility;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class DefaultEventBus : Disposable, IEventBus
	{
		private static readonly Dictionary<Type, List<object>> _handlers = new Dictionary<Type, List<object>>();
		private readonly object _queueLock = new object();
		private readonly Queue<dynamic> _eventQueue = new Queue<dynamic>();

		static DefaultEventBus()
		{
			var concreteTypes = BuildManagerWrapper.Current.ConcreteTypes;
			var eventTypes = concreteTypes
				.Where(t => typeof(IEvent).IsAssignableFrom(t));

			foreach (var eventType in eventTypes)
			{
				var genericHandlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

				if (genericHandlerType != null)
				{
					var handlerTypes = concreteTypes
						.Where(t => genericHandlerType.IsAssignableFrom(t));

					foreach (var handlerType in handlerTypes)
					{
						var handler = Activator.CreateInstance(handlerType);

						if (_handlers.ContainsKey(eventType))
						{
							var registeredHandlers = _handlers[eventType];

							if (registeredHandlers != null)
							{
								if (!registeredHandlers.Contains(handler))
								{
									registeredHandlers.Add(handler);
								}
							}
							else
							{
								_handlers.Add(eventType, new List<object> { handler });
							}
						}
						else
						{
							_handlers.Add(eventType, new List<object> { handler });
						}
					}
				}
			}
		}

		public void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
		{
			lock (_queueLock)
			{
				_eventQueue.Enqueue(@event);
			}
		}

		#region IUnitOfWork

		public void Commit()
		{
			lock (_queueLock)
			{
				while (_eventQueue.Count > 0)
				{
					this.HandleEvent(_eventQueue.Dequeue());
				}
			}
		}

		private void HandleEvent<TEvent>(TEvent @event) where TEvent : class, IEvent
		{
			var eventType = @event.GetType();

			if (_handlers.ContainsKey(eventType))
			{
				var eventHandlers = _handlers[eventType];

				foreach (var eventHandler in eventHandlers)
				{
					var handler = (IEventHandler<TEvent>)eventHandler;

					handler.Handle(@event);
				}
			}
		}

		public void Rollback()
		{
			lock (_queueLock)
			{
				_eventQueue.Clear();
			}
		}

		#endregion
	}
}
