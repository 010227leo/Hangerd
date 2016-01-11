using System;

namespace Hangerd.Uow
{
	public interface IUnitOfWork : IDisposable
	{
		event EventHandler Completed;

		string Id { get; }

		void Commit();
	}
}
