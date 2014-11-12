namespace Hangerd.Components
{
	using System;
	using System.IO;
	using System.Text;
	using System.Threading;

	public class LocalLoggingService
	{
		private static object _locker = new object();
		private static StreamWriter _streamWriter;
		private static Timer _changePathTimer;
		private static readonly int _changePathInterval = 15 * 60 * 1000;

		internal static void Init()
		{
			_changePathTimer = new Timer(state =>
			{
				lock (_locker)
				{
					Close();
					InitStreamWriter();
				}
			}, null, _changePathInterval, _changePathInterval);

			InitStreamWriter();
		}

		internal static void Close()
		{
			if (_streamWriter != null)
			{
				_streamWriter.Close();
			}
		}

		private static void InitStreamWriter()
		{
			_streamWriter = new StreamWriter(GetLogFileName(), true, Encoding.UTF8, 1024);
			_streamWriter.AutoFlush = true;
		}

		private static string GetLogFileName()
		{
			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			var file = string.Format("{0}.log", DateTime.Now.ToString("yyyyMMdd"));

			return Path.Combine(path, file);
		}

		private static void AddLog(LogLevel logType, string msg)
		{
			lock (_locker)
			{
				var log = string.Format("[{0}] @{1}: - {2}", 
					logType.ToString(), DateTime.Now.ToString("HH:mm:ss.ffff"), msg);

				_streamWriter.WriteLine(log);

				Console.WriteLine(log);
			}
		}

		public static void Info(string logFormat, params object[] args)
		{
			Info(string.Format(logFormat, args));
		}

		public static void Info(string log)
		{
			AddLog(LogLevel.Info, log);
		}

		public static void Debug(string logFormat, params object[] args)
		{
			Debug(string.Format(logFormat, args));
		}

		public static void Debug(string log)
		{
			AddLog(LogLevel.Debug, log);
		}

		public static void Exception(string logFormat, params object[] args)
		{
			Exception(string.Format(logFormat, args));
		}

		public static void Exception(Exception exception)
		{
			if (exception == null)
			{
				return;
			}

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

		public static void Exception(string log)
		{
			AddLog(LogLevel.Exception, log);
		}
	}

	public enum LogLevel
	{
		Info = 1,

		Debug = 2,

		Exception = 3
	}
}
