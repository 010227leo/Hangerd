using Hangerd.Utility;
using NUnit.Framework;

namespace Hangerd.Test.Utility
{
	public class ValidationHelperTest
	{
		[Test]
		public void IsEmailAddressTest()
		{
			const string inputRight = "010227leo@Gmail.com";
			const string inputWrong = "http://www.google.com";

			Assert.IsTrue(ValidationHelper.IsEmailAddress(inputRight));
			Assert.IsFalse(ValidationHelper.IsEmailAddress(inputWrong));
		}

		[Test]
		public void IsIdentityNumberTest()
		{
			const string inputRight = "37010219880807115x";
			const string inputWrong = "37010219880807115a";

			Assert.IsTrue(ValidationHelper.IsIdentityNumber(inputRight));
			Assert.IsFalse(ValidationHelper.IsIdentityNumber(inputWrong));
		}

		[Test]
		public void IsMobileNumberTest()
		{
			const string inputRight = "13812345678";
			const string inputWrong = "a1s2d3e@google.com";

			Assert.IsTrue(ValidationHelper.IsMobileNumber(inputRight));
			Assert.IsFalse(ValidationHelper.IsMobileNumber(inputWrong));
		}

		[Test]
		public void IsUrlAddressTest()
		{
			const string inputRight = "http://api.efufc.com/login?returnUrl=null";
			const string inputWrong = "010227leo@Gmail.com";

			Assert.IsTrue(ValidationHelper.IsUrlAddress(inputRight));
			Assert.IsFalse(ValidationHelper.IsUrlAddress(inputWrong));
		}
	}
}
