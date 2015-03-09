using System;

namespace Hangerd.Test
{
	public interface ITestService : IDisposable
	{
		string ServiceId { get; }

		bool Disposed { get; }
	}

	public class TestService : ITestService
	{
		private readonly string _serviceId;

		public string ServiceId
		{
			get { return _serviceId; }
		}

		public bool Disposed { get; private set; }

		public TestService()
		{
			_serviceId = Guid.NewGuid().ToString("N");
		}

		public void Dispose()
		{
			Disposed = true;
		}
	}
}
