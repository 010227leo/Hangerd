using System;

namespace Hangerd.Uow
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
	}
}
