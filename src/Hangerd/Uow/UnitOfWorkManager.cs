using Hangerd.Components;

namespace Hangerd.Uow
{
	public static class UnitOfWorkManager
	{
		public static TUnitOfWork Begin<TUnitOfWork>()
			where TUnitOfWork : class, IUnitOfWork
		{
			var provider = LocalServiceLocator.GetService<ICurrentUowProvider>();
			var uow = LocalServiceLocator.GetService<TUnitOfWork>();

			provider.SetCurrent(uow);

			return uow;
		}
	}
}
