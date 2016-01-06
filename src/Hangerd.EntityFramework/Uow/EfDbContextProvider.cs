using Hangerd.Domain.Repository;
using Hangerd.Uow;

namespace Hangerd.EntityFramework.Uow
{
    public class EfDbContextProvider<TDbContext> : IEfDbContextProvider<TDbContext> 
        where TDbContext : HangerdDbContext
    {
		private readonly ICurrentUowProvider _currentUowProvider;

		public TDbContext DbContext { get { return _currentUowProvider.GetCurrent<IRepositoryUow>().GetDbContext<TDbContext>(); } }

		public EfDbContextProvider(ICurrentUowProvider currentUowProvider)
        {
			_currentUowProvider = currentUowProvider;
        }
    }
}