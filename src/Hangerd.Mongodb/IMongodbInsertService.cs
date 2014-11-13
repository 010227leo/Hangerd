namespace Hangerd.Mongodb
{
	using System;

	public interface IMongodbInsertService : IDisposable
	{
		void Insert(MongodbItem item);
	}
}
