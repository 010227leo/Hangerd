namespace Hangerd.Test.Utility
{
	using Hangerd.Utility;
	using NUnit.Framework;

	public class CommonToolsTest
	{
		enum TestEnum
		{
			[System.ComponentModel.Description("FirstDayOfWeek")]
			Sunday = 1,

			[System.ComponentModel.Description("SecondDayOfWeek")]
			Monday = 2
		}

		[Test]
		public void GetDescriptionTest()
		{
			const TestEnum monday = TestEnum.Monday;

			var description = CommonTools.GetEnumDescription(monday);

			Assert.AreEqual("SecondDayOfWeek", description);
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
