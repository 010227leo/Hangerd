namespace Hangerd.Mongodb.Imp
{
	using MongoDB.Driver;

	public class MongodbServiceBase : Disposable
	{
		private MongoServer _cachedMongoServer;

		protected MongoServer CachedMongoServer
		{
			get
			{
				if (_cachedMongoServer == null)
				{
					var config = MongodbConfig.Current;

					if (config != null && !string.IsNullOrWhiteSpace(config.MongodbServiceAddress))
					{
						var connectionString = string.Format("mongodb://{0}", config.MongodbServiceAddress);
						var client = new MongoClient(connectionString);

						_cachedMongoServer = client.GetServer();
					}
				}

				return _cachedMongoServer;
			}
		}
	}
}
