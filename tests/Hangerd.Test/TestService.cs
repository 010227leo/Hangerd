namespace Hangerd.Test
{
	using System;

	public class TestService : ITestService
	{
		private string _serviceId;

		public string ServiceId
		{
			get { return _serviceId; }
		}

		private bool _disposed;

		public bool Disposed
		{
			get { return _disposed; }
		}

		public TestService()
		{
			_serviceId = Guid.NewGuid().ToString("N");
		}

		public void Dispose()
		{
			_disposed = true;
		}
	}
}
