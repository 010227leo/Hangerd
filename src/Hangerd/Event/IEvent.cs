using System;

namespace Hangerd.Event
{
	public interface IEvent
	{
		Guid Id { get; }

		DateTime Timestamp { get; }
	}
}
