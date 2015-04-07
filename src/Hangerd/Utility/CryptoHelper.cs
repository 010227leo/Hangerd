using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Hangerd.Utility
{
	public static class CryptoHelper
	{
		private const string Iv = "#kRz4rK^Z#kLMgQ^!ZHsY0u6";
		private const string Key = "S()%s@z^";

		public static string Encrypt(string input)
		{
			var btKey = Encoding.Default.GetBytes(Key);
			var btIv = Encoding.Default.GetBytes(Iv);
			var des = new DESCryptoServiceProvider();

			using (var ms = new MemoryStream())
			{
				var inData = Encoding.Default.GetBytes(input);

				using (var cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIv), CryptoStreamMode.Write))
				{
					cs.Write(inData, 0, inData.Length);
					cs.FlushFinalBlock();
				}

				return Convert.ToBase64String(ms.ToArray());
			}
		}

		public static string Decrypt(string input)
		{
			var btKey = Encoding.Default.GetBytes(Key);
			var btIv = Encoding.Default.GetBytes(Iv);
			var des = new DESCryptoServiceProvider();

			using (var ms = new MemoryStream())
			{
				var inData = Convert.FromBase64String(input);

				using (var cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIv), CryptoStreamMode.Write))
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

			foreach (var t in hash)
				md5Builder.Append(t.ToString("X2"));

			return md5Builder.ToString();
		}

		public static string GetSha1(string input)
		{
			var sha1 = SHA1.Create();
			var inputBytes = Encoding.ASCII.GetBytes(input);
			var hash = sha1.ComputeHash(inputBytes);

			var sha1Builder = new StringBuilder();

			foreach (var t in hash)
				sha1Builder.Append(t.ToString("X2"));

			return sha1Builder.ToString();
		}
	}
}
