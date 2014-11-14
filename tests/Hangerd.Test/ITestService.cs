namespace Hangerd.Test
{
	using System;

	public interface ITestService : IDisposable
	{

		string ServiceId { get; }

		bool Disposed { get; }
	}
}
