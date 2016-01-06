namespace Hangerd.EntityFramework
{
	public interface IDbContextProvider<out TDbContext>
        where TDbContext : HangerdDbContext
    {
        TDbContext DbContext { get; }
    }
}