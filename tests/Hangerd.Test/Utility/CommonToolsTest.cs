using Hangerd.Utility;
using NUnit.Framework;

namespace Hangerd.Test.Utility
{
	public class CommonToolsTest
	{
		enum TestEnum
		{
			[System.ComponentModel.Description("FirstDayOfWeek")]
			Sunday
		}

		[Test]
		public void GetDescriptionTest()
		{
			const TestEnum sunday = TestEnum.Sunday;

			var description = CommonTools.GetEnumDescription(sunday);

			Assert.AreEqual("FirstDayOfWeek", description);
		}

		[Test]
		public void CutStringTest()
		{
			const string input = "010227leo@gmail.com";

			Assert.AreEqual("010227leo@#", CommonTools.CutString(input, 10, "#"));
		}

		[Test]
		public void FilterHtmlTest()
		{
			const string input = @"<html><header><script>alert('test');</script></header><body>hello world!</body></html>";

			Assert.AreEqual("hello world!", CommonTools.FilterHtml(input));
		}
	}
}
