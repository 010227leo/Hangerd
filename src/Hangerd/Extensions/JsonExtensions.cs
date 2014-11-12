namespace Hangerd.Extensions
{
	using Newtonsoft.Json;

	public static class JsonExtensions
	{
		public static string ObjectToJson(this object obj, Formatting formatting = Formatting.None)
		{
			return JsonConvert.SerializeObject(obj, formatting);
		}

		public static T JsonToObject<T>(this string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return default(T);
			}

			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
