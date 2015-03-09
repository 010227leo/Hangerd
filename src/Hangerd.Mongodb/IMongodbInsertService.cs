using System;

namespace Hangerd.Mongodb
{
	public interface IMongodbInsertService : IDisposable
	{
		void Insert(MongodbItem item);
	}
}
