using System;

namespace Hangerd
{
	public abstract class Disposable : IDisposable
	{
		private bool _disposed;

		~Disposable()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void InternalDispose() { }

		private void Dispose(bool disposing)
		{
			if (!_disposed && disposing)
				InternalDispose();

			_disposed = true;
		}
	}
}
