using System;

namespace Hangerd.Event
{
	public interface IEvent
	{
		Guid Id { get; set; }

		DateTime Timestamp { get; set; }
	}
}
