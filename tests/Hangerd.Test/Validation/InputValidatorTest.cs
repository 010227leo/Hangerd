using Hangerd.Validation;
using NUnit.Framework;

namespace Hangerd.Test.Validation
{
	public class InputValidatorTest
	{
		[Test]
		public void IsEmailAddressTest()
		{
			const string inputRight = "010227leo@Gmail.com";
			const string inputWrong = "http://www.google.com";

			Assert.IsTrue(InputValidator.IsEmailAddress(inputRight));
			Assert.IsFalse(InputValidator.IsEmailAddress(inputWrong));
		}

		[Test]
		public void IsIdentityNumberTest()
		{
			const string inputRight = "37010219880807115x";
			const string inputWrong = "37010219880807115a";

			Assert.IsTrue(InputValidator.IsIdentityNumber(inputRight));
			Assert.IsFalse(InputValidator.IsIdentityNumber(inputWrong));
		}

		[Test]
		public void IsMobileNumberTest()
		{
			const string inputRight = "13812345678";
			const string inputWrong = "a1s2d3e@google.com";

			Assert.IsTrue(InputValidator.IsMobileNumber(inputRight));
			Assert.IsFalse(InputValidator.IsMobileNumber(inputWrong));
		}

		[Test]
		public void IsUrlAddressTest()
		{
			const string inputRight = "http://api.efufc.com/login?returnUrl=null";
			const string inputWrong = "010227leo@Gmail.com";

			Assert.IsTrue(InputValidator.IsUrlAddress(inputRight));
			Assert.IsFalse(InputValidator.IsUrlAddress(inputWrong));
		}
	}
}
