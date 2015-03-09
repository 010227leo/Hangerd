using System;

namespace Hangerd
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();

		void Rollback();
	}
}
