using Newtonsoft.Json;

namespace Hangerd.Extensions
{
	public static class JsonExtensions
	{
		public static string ObjectToJson(this object obj, bool indented = false)
		{
			var options = new JsonSerializerSettings();

			if (indented)
				options.Formatting = Formatting.Indented;

			return JsonConvert.SerializeObject(obj, options);
		}

		public static T JsonToObject<T>(this string json)
		{
			return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
		}
	}
}
