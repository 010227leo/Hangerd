namespace Hangerd.Validation
{
	public static class Requires
	{
		public static void NotNull<T>([ValidatedNotNull] T value, string message)
			where T : class
		{
			if (value == null)
				ThrowException(message);
		}

		public static void IsTrue(bool condition, string message)
		{
			if (!condition)
				ThrowException(message);
		}

		private static void ThrowException(string message)
		{
			throw new HangerdException(message);
		}
	}
}
