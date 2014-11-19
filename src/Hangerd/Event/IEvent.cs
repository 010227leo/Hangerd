namespace Hangerd.Event
{
	using System;

	public interface IEvent
	{
		Guid Id { get; set; }

		DateTime Timestamp { get; set; }
	}
}
