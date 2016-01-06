namespace Hangerd.EntityFramework.Uow
{
	public interface IEfDbContextProvider<out TDbContext>
        where TDbContext : HangerdDbContext
    {
        TDbContext DbContext { get; }
    }
}