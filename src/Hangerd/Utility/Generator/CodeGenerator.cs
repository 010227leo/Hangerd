namespace Hangerd.Utility.Generator
{
	using System;

	public class CodeGenerator
	{
		private static readonly char[] _mixedClear = { 
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 
			'I', 'J', 'K', 'L', 'M', 'N', 'P', 'Q',
			'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z',
			'2', '3', '4', '5', '6', '7', '8', '9'
		};

		private static readonly char[] _mixedAll = { 
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',  
			'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',  
			'q', 'r', 's', 't', 'u', 'v', 'w', 'x',  
			'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 
			'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 
			'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 
			'W', 'X', 'Y', 'Z', '0', '1', '2', '3', 
			'4', '5', '6', '7', '8', '9',
		};

		private static readonly char[] _lettersUpper = { 
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
			'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
			'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 
			'Y', 'Z' 
		};

		private static readonly char[] _lettersLower = { 
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
			'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 
			'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 
			'y', 'z' 
		};

		private static readonly char[] _number = { 
			'0', '1', '2', '3', '4', '5', '6', '7', 
			'8', '9' 
		};

		public static string Create(int length, CharacterType type = CharacterType.MixedClear)
		{
			if (length < 1 || length > 16)
			{
				throw new ArgumentOutOfRangeException("length");
			}

			char[] source;

			switch (type)
			{
				case CharacterType.MixedAll:
					source = _mixedAll;
					break;
				case CharacterType.LettersUpper:
					source = _lettersUpper;
					break;
				case CharacterType.LettersLower:
					source = _lettersLower;
					break;
				case CharacterType.Number:
					source = _number;
					break;
				default:
					source = _mixedClear;
					break;
			}

			var sequence = Guid.NewGuid().ToByteArray();
			var output = new char[length];

			for (var i = 0; i < length; i++)
			{
				output[i] = source[sequence[i] % source.Length];
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
