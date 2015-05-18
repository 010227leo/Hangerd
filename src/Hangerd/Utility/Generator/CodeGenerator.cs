using System;
using System.Collections.Generic;
using System.Text;

namespace Hangerd.Utility.Generator
{
	public class CodeGenerator
	{
		private static readonly Dictionary<CharacterType, string> CodeSet = new Dictionary<CharacterType, string>
		{
			{ CharacterType.MixedClear, "ABCDEFGHIJKLMNPQRSTVWXYZ23456789" },
			{ CharacterType.MixedAll, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" },
			{ CharacterType.LettersUpper, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
			{ CharacterType.LettersLower, "abcdefghijklmnopqrstuvwxyz" },
			{ CharacterType.Number, "0123456789" }
		};

		public static string Create(int length, CharacterType type = CharacterType.MixedClear)
		{
			return Create(length, CodeSet[type]);
		}

		public static string Create(int length, string source)
		{
			if (string.IsNullOrWhiteSpace(source))
				throw new ArgumentNullException("source");

			var output = new StringBuilder(length);

			for (var i = 0; i * 16 < length; i++)
			{
				var sequence = Guid.NewGuid().ToByteArray();
				var partLength = Math.Min(16, length - i * 16);

				for (var j = 0; j < partLength; j++)
					output.Append(source[sequence[j] % source.Length]);
			}

			return output.ToString();
		}
	}

	public enum CharacterType
	{
		MixedClear,
		MixedAll,
		LettersUpper,
		LettersLower,
		Number
	}
}
