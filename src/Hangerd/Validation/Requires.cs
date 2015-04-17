using System.Diagnostics;

namespace Hangerd.Validation
{
	public static class Requires
	{
		[DebuggerStepThrough]
		public static void NotNull<T>([ValidatedNotNull] T value, string message)
			where T : class
		{
			if (value == null)
				ThrowException(message);
		}

		[DebuggerStepThrough]
		public static void IsTrue(bool condition, string message)
		{
			if (!condition)
				ThrowException(message);
		}

		[DebuggerStepThrough]
		private static void ThrowException(string message)
		{
			throw new HangerdException(message);
		}
	}
}
