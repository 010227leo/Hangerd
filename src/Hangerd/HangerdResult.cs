namespace Hangerd
{
	public class HangerdResult<T>
	{
		public T Value { get; private set; }

		public string Message { get; private set; }

		public HangerdResult(T value, string message)
		{
			Value = value;
			Message = message;
		}
	}
}
