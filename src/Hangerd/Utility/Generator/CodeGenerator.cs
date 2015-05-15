using System;
using System.Collections.Generic;

namespace Hangerd.Utility.Generator
{
	public class CodeGenerator
	{
		private static readonly Dictionary<CharacterType, char[]> CodeSet = new Dictionary<CharacterType, char[]>
		{
			{
				CharacterType.MixedClear, new[]
				{
					'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
					'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V',
					'W', 'X', 'Y', 'Z', '2', '3', '4', '5', '6', '7',
					'8', '9'
				}
			},
			{
				CharacterType.MixedAll, new[]
				{
					'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
					'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
					'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
					'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
					'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
					'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7',
					'8', '9',
				}
			},
			{
				CharacterType.LettersUpper, new[]
				{
					'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
					'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
					'U', 'V', 'W', 'X', 'Y', 'Z'
				}
			},
			{
				CharacterType.LettersLower, new[]
				{
					'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
					'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
					'u', 'v', 'w', 'x', 'y', 'z'
				}
			},
			{
				CharacterType.Number, new[]
				{
					'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
				}
			}
		};

		public static string Create(int length, CharacterType type = CharacterType.MixedClear)
		{
			var source = CodeSet[type];
			var sequence = Guid.NewGuid().ToByteArray();
			var output = new char[length];
			var first16 = Math.Min(16, length);

			for (var i = 0; i < first16; i++)
				output[i] = source[sequence[i] % source.Length];

			if (length > 16)
			{
				var random = new Random(Guid.NewGuid().GetHashCode());

				for (var i = 16; i < length; i++)
					output[i] = source[random.Next(source.Length)];
			}

			return new string(output);
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
