namespace Hangerd
{
	using System;

	public class HangerdException: Exception
    {
        public HangerdException() : base() { }
     
        public HangerdException(string message) : base(message) { }
      
        public HangerdException(string message, Exception innerException) : base(message, innerException) { }

		public HangerdException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
