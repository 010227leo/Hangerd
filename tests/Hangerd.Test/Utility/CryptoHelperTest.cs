namespace Hangerd.Test.Utility
{
	using Hangerd.Utility;
	using NUnit.Framework;

	public class CryptoHelperTest
	{
		[Test]
		public void EncryptTest()
		{
			const string input = "010227leo@gmail.com";

			Assert.AreEqual("GcbGyYSDNPO8C30jri7mJtneYteMac+u", CryptoHelper.Encrypt(input));
		}

		[Test]
		public void DecryptTest()
		{
			const string input = "GcbGyYSDNPO8C30jri7mJtneYteMac+u";

			Assert.AreEqual("010227leo@gmail.com", CryptoHelper.Decrypt(input));
		}

		[Test]
		public void GetMd5Test()
		{
			const string input = "010227leo@gmail.com";

			Assert.AreEqual("e963c0045f349ba417d32d0cf28fe72d", CryptoHelper.GetMd5(input).ToLower());
		}

		[Test]
		public void GetSha1Test()
		{
			const string input = "010227leo@gmail.com";

			Assert.AreEqual("5c6829606eeb729f970bf2f60682c0a9763b19a1", CryptoHelper.GetSha1(input).ToLower());
		}
	}
}
