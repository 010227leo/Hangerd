using Hangerd.Utility;
using NUnit.Framework;

namespace Hangerd.Test.Utility
{
	public class CommonToolsTest
	{
		private enum TestEnum
		{
			[System.ComponentModel.Description("Enum1")] Enum1,
			[System.ComponentModel.Description("Enum2")] Enum2,
			[System.ComponentModel.Description("Enum3")] Enum3
		}

		[Test]
		public void GetDescriptionTest()
		{

			const TestEnum testEnum = TestEnum.Enum3;

			var description = CommonTools.GetEnumDescription(testEnum);

			Assert.AreEqual("Enum3", description);
		}

		[Test]
		public void ForEachEnumTest()
		{
			var enumCount = 0;

			CommonTools.ForEachEnum(typeof(TestEnum), value =>
			{
				enumCount++;
			});

			Assert.AreEqual(3, enumCount);
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
