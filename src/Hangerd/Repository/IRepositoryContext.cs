namespace Hangerd.Repository
{
	using System.Collections.Generic;

	public interface IRepositoryContext : IUnitOfWork
    {
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

		int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}
