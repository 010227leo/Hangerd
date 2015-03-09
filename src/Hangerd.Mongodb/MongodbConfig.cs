using Hangerd.Components;

namespace Hangerd.Mongodb
{
	public class MongodbConfig
	{
		public string MongodbServiceAddress { get; set; }

		public static MongodbConfig Current
		{
			get
			{
				return LocalConfigService.GetConfig(new MongodbConfig
				{
					MongodbServiceAddress = "localhost",
				});
			}
		}
	}
}
