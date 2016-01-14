using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Hangerd.Components
{
	public static class LocalLoggingService
	{
		private const int ChangePathInterval = 15 * 60 * 1000;
		private static readonly object Locker = new object();
		private static Timer _timer;
		private static StreamWriter _streamWriter;

		internal static void Init()
		{
			_timer = new Timer(state =>
			{
				CloseStreamWriter();
				InitStreamWriter();
			}, null, ChangePathInterval, ChangePathInterval);

			InitStreamWriter();
		}

		internal static void Dispose()
		{
			CloseStreamWriter();

			_timer.Dispose();
		}

		private static void CloseStreamWriter()
		{
			lock (Locker)
			{
				if (_streamWriter != null)
					_streamWriter.Close();
			}
		}

		private static void InitStreamWriter()
		{
			lock (Locker)
			{
				_streamWriter = new StreamWriter(GetLogFileName(), true, Encoding.UTF8, 1024)
				{
					AutoFlush = true
				};
			}
		}

		private static string GetLogFileName()
		{
			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			var file = string.Format("{0}.log", DateTime.Now.ToString("yyyyMMdd"));

			return Path.Combine(path, file);
		}

		public static void Info(string logFormat, params object[] args)
		{
			Info(string.Format(logFormat, args));
		}

		public static void Info(string message)
		{
			InternalAddLog(LogLevel.Info, message);
		}

		public static void Debug(string logFormat, params object[] args)
		{
			Debug(string.Format(logFormat, args));
		}

		public static void Debug(string message)
		{
			InternalAddLog(LogLevel.Debug, message);
		}

		public static void Exception(Exception exception)
		{
			if (exception == null)
				return;

			Exception(exception.Message);
			Exception(exception.StackTrace);

			var innerException = exception.InnerException;

			while (innerException != null)
			{
				Exception(innerException.Message);
				Exception(innerException.StackTrace);

				innerException = innerException.InnerException;
			}
		}

		public static void Exception(string logFormat, params object[] args)
		{
			Exception(string.Format(logFormat, args));
		}

		public static void Exception(string message)
		{
			InternalAddLog(LogLevel.Exception, message);
		}

		private static void InternalAddLog(LogLevel logType, string message)
		{
			lock (Locker)
			{
				if (_streamWriter == null)
					throw new Exception("StreamWriter has not init.");

				var log = string.Format("[{0}] @{1}: - {2}", logType, DateTime.Now.ToString("HH:mm:ss.ffff"), message);

				_streamWriter.WriteLine(log);

				Console.WriteLine(log);
			}
		}
	}

	public enum LogLevel
	{
		Info,
		Debug,
		Exception
	}
}
