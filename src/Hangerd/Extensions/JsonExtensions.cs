using Newtonsoft.Json;

namespace Hangerd.Extensions
{
	public static class JsonExtensions
	{
		public static string ObjectToJson(this object obj, Formatting formatting = Formatting.None)
		{
			return JsonConvert.SerializeObject(obj, formatting);
		}

		public static T JsonToObject<T>(this string json)
		{
			return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
		}
	}
}
