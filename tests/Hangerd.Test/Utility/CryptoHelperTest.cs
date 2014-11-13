namespace Hangerd.Test.Utility
{
	using Hangerd.Utility;
	using NUnit.Framework;

	public class CryptoHelperTest
	{
		[Test]
		public void EncryptTest()
		{
			var input = "010227leo@gmail.com";
			var output = CryptoHelper.Encrypt(input);

			Assert.AreEqual("GcbGyYSDNPO8C30jri7mJtneYteMac+u", output);
		}

		[Test]
		public void DecryptTest()
		{
			var input = "GcbGyYSDNPO8C30jri7mJtneYteMac+u";
			var output = CryptoHelper.Decrypt(input);

			Assert.AreEqual("010227leo@gmail.com", output);
		}

		[Test]
		public void GetMd5Test()
		{
			var input = "010227leo@gmail.com";

			Assert.AreEqual("e963c0045f349ba417d32d0cf28fe72d", CryptoHelper.GetMd5(input).ToLower());
		}

		[Test]
		public void GetSha1Test()
		{
			var input = "010227leo@gmail.com";

			Assert.AreEqual("5c6829606eeb729f970bf2f60682c0a9763b19a1", CryptoHelper.GetSha1(input).ToLower());
		}
	}
}
