namespace Hangerd.Utility
{
	using System.Text.RegularExpressions;

	public class ValidationHelper
	{
		private const string _identityNumberPattern = @"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$";
		private const string _mobileNumberPattern = @"^1[3|5|8][0-9]\d{8}$";
		private const string _emailAddressPattern = @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";
		private const string _urlAddressPattern = @"[a-zA-z]+://[^\s]*";

		public static bool IsIdentityNumber(string input)
		{
			return !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, _identityNumberPattern, RegexOptions.IgnoreCase);
		}

		public static bool IsMobileNumber(string input)
		{
			return !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, _mobileNumberPattern, RegexOptions.IgnoreCase);
		}

		public static bool IsEmailAddress(string input)
		{
			return !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, _emailAddressPattern, RegexOptions.IgnoreCase);
		}

		public static bool IsUrlAddress(string input)
		{
			return !string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, _urlAddressPattern, RegexOptions.IgnoreCase);
		}
	}
}
