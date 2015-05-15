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
			var hashSet = new HashSet<string>();

			for (var i = 0; i < 100000; i++)
			{
				var code = CodeGenerator.Create(8);

				if (hashSet.Contains(code))
					Assert.Fail("code is exsit at {0}", i);

				hashSet.Add(code);
			}

			Assert.Pass();
		}
	}
}
