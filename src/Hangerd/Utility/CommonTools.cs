﻿namespace Hangerd.Utility
{
	using System;
	using System.ComponentModel;
	using System.Text;
	using System.Text.RegularExpressions;

	public class CommonTools
	{
		public static string CutString(string input, int limitLength, string suffix)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return string.Empty;
			}

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
				else
				{
					output += ch;
				}
			}

			return output;
		}

		public static string FilterHtml(string input)
		{
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

		public static string GetEnumDescription(Enum value)
		{
			var description = string.Empty;
			var field = value.GetType().GetField(value.ToString());

			if (field != null)
			{
				var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attributes.Length > 0)
				{
					description = ((DescriptionAttribute)attributes[0]).Description;
				}
			}

			return description;
		}
	}
}
