namespace Hangerd.UnitOfWork
{
	using System;

	public interface IUnitOfWork : IDisposable
	{
		void Commit();

		void Rollback();
	}
}
