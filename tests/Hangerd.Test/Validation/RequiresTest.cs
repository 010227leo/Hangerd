using Hangerd.Test.Domain.Entity;
using Hangerd.Validation;
using NUnit.Framework;

namespace Hangerd.Test.Validation
{
	public class RequiresTest
	{
		[Test]
		public void NotNullTest()
		{
			try
			{
				Requires.NotNull<SampleEntity>(null, "can not be null");

				Assert.Fail();
			}
			catch (HangerdException ex)
			{
				Assert.AreEqual("can not be null", ex.Message);
			}
		}

		[Test]
		public void IsTrueTest()
		{
			try
			{
				Requires.IsTrue(false, "can not be false");

				Assert.Fail();
			}
			catch (HangerdException ex)
			{
				Assert.AreEqual("can not be false", ex.Message);
			}
		}
	}
}
