namespace Hangerd.Uow
{
	public interface ICurrentUowProvider
	{
		TUnitOfWork GetCurrent<TUnitOfWork>() where TUnitOfWork : class, IUnitOfWork;

		void SetCurrent<TUnitOfWork>(TUnitOfWork value) where TUnitOfWork : class, IUnitOfWork;
	}
}