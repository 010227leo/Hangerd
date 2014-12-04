namespace Hangerd.Test.Utility.Generator
{
	using Hangerd.Utility.Generator;
	using NUnit.Framework;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	public class CodeGeneratorTest
	{
		[Test]
		public void CommonTest()
		{
			Console.WriteLine("MixedClear:		{0}", CodeGenerator.Create(16, CharacterType.MixedClear));
			Console.WriteLine("MixedAll:		{0}", CodeGenerator.Create(16, CharacterType.MixedAll));
			Console.WriteLine("LettersUpper:	{0}", CodeGenerator.Create(16, CharacterType.LettersUpper));
			Console.WriteLine("LettersLower:	{0}", CodeGenerator.Create(16, CharacterType.LettersLower));
			Console.WriteLine("Number:			{0}", CodeGenerator.Create(16, CharacterType.Number));
		}

		[Test]
		public void ConflictTest()
		{
			var hasConflict = false;
			var hashTable = new HashSet<string>();

			for (var i = 0; i < 100000; i++)
			{
				var code = CodeGenerator.Create(8);

				if (hashTable.Contains(code))
				{
					hasConflict = true;

					Console.WriteLine("{0} is exist! i = {1}", code, i);

					break;
				}
				else
				{
					hashTable.Add(code);
				}
			}

			Assert.IsFalse(hasConflict);
		}
	}
}
