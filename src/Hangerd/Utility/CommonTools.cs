using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Hangerd.Utility
{
	public static class CommonTools
	{
		public static string CutString(string input, int limitLength, string suffix)
		{
			if (string.IsNullOrWhiteSpace(input))
				return string.Empty;

			var output = string.Empty;
			var currentLength = 0;

			foreach (var ch in input)
			{
				currentLength += Encoding.Default.GetByteCount(ch.ToString());

				if (currentLength > limitLength)
				{
					output += suffix;

					return output;
				}

				output += ch;
			}

			return output;
		}

		public static string FilterHtml(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				return string.Empty;

			var output = input;

			output = Regex.Replace(output, @"<script[^>]*?>.*?</script>", string.Empty, RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"<(.[^>]*)>", string.Empty, RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"([\r\n])[\s]+", string.Empty, RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"–>", string.Empty, RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"<!–.*", string.Empty, RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			output = Regex.Replace(output, @"&#(\d+);", string.Empty, RegexOptions.IgnoreCase);

			return output;
		}

		public static string GetEnumDescription(object value, char separator = ',')
		{
			var enumType = value.GetType();

			if (!enumType.IsEnum)
				return string.Empty;

			var flagAttributes = enumType.GetCustomAttributes(typeof (FlagsAttribute), false);

			if (flagAttributes.Length == 0)
				return GetDescription(enumType, value);

			//FlagsAttribute
			var fields = enumType.GetEnumValues();
			var descriptionBuilder = new StringBuilder();

			foreach (var field in fields)
			{
				if (((int) value & (int) field) == (int) field)
					descriptionBuilder.AppendFormat("{0}{1}", GetDescription(enumType, field), separator);
			}

			return descriptionBuilder.ToString().TrimEnd(separator);
		}

		private static string GetDescription(Type enumType, object value)
		{
			var field = enumType.GetField(value.ToString());

			if (field == null)
				return string.Empty;

			var attributes = field.GetCustomAttributes(typeof (DescriptionAttribute), false);

			return attributes.Length > 0 ? ((DescriptionAttribute) attributes[0]).Description : string.Empty;
		}

		public static void ForEachEnum(Type enumType, Action<object> action)
		{
			if (!enumType.IsEnum)
				return;

			var values = Enum.GetValues(enumType);

			foreach (var value in values)
				action(value);
		}
	}
}
