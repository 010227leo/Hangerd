namespace Hangerd.Repository
{
	public interface IRepositoryFactory
	{
		TRepository CreateRepository<TRepository>(IRepositoryContext context);
	}
}
