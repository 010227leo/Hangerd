using System.Text.RegularExpressions;

namespace Hangerd.Utility
{
	public static class ValidationHelper
	{
		public static bool IsIdentityNumber(string input)
		{
			return !string.IsNullOrWhiteSpace(input) &&
			       Regex.IsMatch(input, @"^(\d{17})([0-9]|X|x)$", RegexOptions.IgnoreCase);
		}

		public static bool IsMobileNumber(string input)
		{
			return !string.IsNullOrWhiteSpace(input) &&
			       Regex.IsMatch(input, @"^1[3|5|7|8]\d{9}$", RegexOptions.IgnoreCase);
		}

		public static bool IsEmailAddress(string input)
		{
			return !string.IsNullOrWhiteSpace(input) &&
			       Regex.IsMatch(input,
				       @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?",
				       RegexOptions.IgnoreCase);
		}

		public static bool IsUrlAddress(string input)
		{
			return !string.IsNullOrWhiteSpace(input) &&
			       Regex.IsMatch(input, @"[a-zA-z]+://[^\s]*", RegexOptions.IgnoreCase);
		}
	}
}
