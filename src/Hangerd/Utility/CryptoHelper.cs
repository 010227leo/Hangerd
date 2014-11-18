namespace Hangerd.Utility
{
	using System;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;

	public class CryptoHelper
	{
		static string _iv = "#kRz4rK^Z#kLMgQ^!ZHsY0u6";
		static string _key = "S()%s@z^";

		public static string Encrypt(string input)
		{
			var btKey = Encoding.Default.GetBytes(_key);
			var btIV = Encoding.Default.GetBytes(_iv);
			var des = new DESCryptoServiceProvider();

			using (MemoryStream ms = new MemoryStream())
			{
				var inData = Encoding.Default.GetBytes(input);

				using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
				{
					cs.Write(inData, 0, inData.Length);
					cs.FlushFinalBlock();
				}

				return Convert.ToBase64String(ms.ToArray());
			}
		}

		public static string Decrypt(string input)
		{
			var btKey = Encoding.Default.GetBytes(_key);
			var btIV = Encoding.Default.GetBytes(_iv);
			var des = new DESCryptoServiceProvider();

			using (MemoryStream ms = new MemoryStream())
			{
				var inData = Convert.FromBase64String(input);

				using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
				{
					cs.Write(inData, 0, inData.Length);
					cs.FlushFinalBlock();
				}

				return Encoding.Default.GetString(ms.ToArray());
			}
		}

		public static string GetMd5(string input)
		{
			var md5 = MD5.Create();
			var inputBytes = Encoding.ASCII.GetBytes(input);
			var hash = md5.ComputeHash(inputBytes);

			var md5Builder = new StringBuilder();

			for (var i = 0; i < hash.Length; i++)
			{
				md5Builder.Append(hash[i].ToString("X2"));
			}

			return md5Builder.ToString();
		}

		public static string GetSha1(string input)
		{
			var sha1 = SHA1.Create();
			var inputBytes = Encoding.ASCII.GetBytes(input);
			var hash = sha1.ComputeHash(inputBytes);

			var sha1Builder = new StringBuilder();

			for (var i = 0; i < hash.Length; i++)
			{
				sha1Builder.Append(hash[i].ToString("X2"));
			}

			return sha1Builder.ToString();
		}
	}
}
