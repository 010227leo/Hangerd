using System.Collections.Generic;
using Hangerd.Utility.Generator;
using NUnit.Framework;

namespace Hangerd.Test.Utility.Generator
{
	public class CodeGeneratorTest
	{
		[Test]
		public void CreateTest()
		{
			var code = CodeGenerator.Create(10, "leo");

			Assert.AreEqual(10, code.Length);

			var clearedCode = code
				.Replace("l", string.Empty)
				.Replace("e", string.Empty)
				.Replace("o", string.Empty);

			Assert.AreEqual(0, clearedCode.Length);
		}

		[Test]
		public void RandomicityTest()
		{
			var hashSet = new HashSet<string>();

			for (var i = 0; i < 1000000; i++)
			{
				var code = CodeGenerator.Create(8, CharacterType.MixedAll);

				if (hashSet.Contains(code))
					Assert.Fail("code is exsit at {0}", i);

				hashSet.Add(code);
			}

			Assert.Pass();
		}
	}
}
