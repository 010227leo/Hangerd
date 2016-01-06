using System;

namespace Hangerd.Uow
{
	public interface IUnitOfWork : IDisposable
	{
		string Id { get; }

		event EventHandler Completed;

		void Commit();
	}
}
