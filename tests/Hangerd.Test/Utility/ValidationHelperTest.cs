namespace Hangerd.Test.Utility
{
	using Hangerd.Utility;
	using NUnit.Framework;

	public class ValidationHelperTest
	{
		[Test]
		public void IsEmailAddressTest()
		{
			var inputRight = "010227leo@Gmail.com";
			var inputWrong = "http://www.google.com";

			Assert.IsTrue(ValidationHelper.IsEmailAddress(inputRight));
			Assert.IsFalse(ValidationHelper.IsEmailAddress(inputWrong));
		}

		[Test]
		public void IsIdentityNumberTest()
		{
			var inputRight = "37010219880807115x";
			var inputWrong = "37010219880807115a";

			Assert.IsTrue(ValidationHelper.IsIdentityNumber(inputRight));
			Assert.IsFalse(ValidationHelper.IsIdentityNumber(inputWrong));
		}

		[Test]
		public void IsMobileNumberTest()
		{
			var inputRight = "13812345678";
			var inputWrong = "a1s2d3e@google.com";

			Assert.IsTrue(ValidationHelper.IsMobileNumber(inputRight));
			Assert.IsFalse(ValidationHelper.IsMobileNumber(inputWrong));
		}

		[Test]
		public void IsUrlAddressTest()
		{
			var inputRight = "http://api.efufc.com/login?returnUrl=null";
			var inputWrong = "010227leo@Gmail.com";

			Assert.IsTrue(ValidationHelper.IsUrlAddress(inputRight));
			Assert.IsFalse(ValidationHelper.IsUrlAddress(inputWrong));
		}
	}
}
