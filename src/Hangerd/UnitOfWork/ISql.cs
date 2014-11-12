namespace Hangerd.UnitOfWork
{
	using System.Collections.Generic;

    public interface ISql
    {
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

		int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}
