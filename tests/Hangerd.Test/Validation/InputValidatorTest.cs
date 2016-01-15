using Hangerd.Validation;
using NUnit.Framework;

namespace Hangerd.Test.Validation
{
	public class InputValidatorTest
	{
		[Test]
		public void IsEmailAddressTest()
		{
			const string inputRight = "leo010227@outlook.com";
			const string inputWrong = "http://api.hangerd.com/signin?code=123456";

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
			const string inputRight = "13000000000";
			const string inputWrong = "leo010227@outlook.com";

			Assert.IsTrue(InputValidator.IsMobileNumber(inputRight));
			Assert.IsFalse(InputValidator.IsMobileNumber(inputWrong));
		}

		[Test]
		public void IsUrlAddressTest()
		{
			const string inputRight = "http://api.hangerd.com/signin?code=123456";
			const string inputWrong = "leo010227@outlook.com";

			Assert.IsTrue(InputValidator.IsUrlAddress(inputRight));
			Assert.IsFalse(InputValidator.IsUrlAddress(inputWrong));
		}
	}
}
