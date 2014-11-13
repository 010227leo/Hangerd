namespace Hangerd.Mongodb
{
	public class MongodbItem
	{
		public string DatabaseName { get; set; }

		public string TableName { get; set; }

		public object Data { get; set; }
	}
}
