namespace Hangerd
{
	using System;

	public interface IUnitOfWork : IDisposable
	{
		void Commit();

		void Rollback();
	}
}
