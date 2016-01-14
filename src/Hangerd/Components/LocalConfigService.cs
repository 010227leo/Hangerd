using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Hangerd.Components
{
	public static class LocalConfigService
	{
		private static readonly string LocalConfigDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");
		private static readonly ConcurrentDictionary<string, object> ConfigCache = new ConcurrentDictionary<string, object>();

		public static T GetConfig<T>(T defaultValue)
		{
			var fileName = string.Format("{0}.config", typeof (T).Name);

			return GetConfig(fileName, defaultValue);
		}

		public static T GetConfig<T>(string fileName, T defaultValue)
		{
			object instance;

			var fileFullName = GetConfigFileFullName(fileName);

			if (ConfigCache.TryGetValue(fileFullName, out instance))
				return (T) instance;

			if (!File.Exists(fileFullName))
			{
				TryCreateConfig(fileName, defaultValue);

				return defaultValue;
			}

			var doc = new XmlDocument();

			try
			{
				doc.Load(fileFullName);
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception("Failed to load config file {0}:{1}", fileFullName, ex.Message);

				return defaultValue;
			}

			var xmlSerializer = new XmlSerializer(typeof (T));

			using (var sr = new StringReader(doc.OuterXml))
			{
				try
				{
					instance = (T) xmlSerializer.Deserialize(sr);

					ConfigCache.TryAdd(fileFullName, instance);

					return (T) instance;
				}
				catch (Exception ex)
				{
					LocalLoggingService.Exception("Failed to deserialize type {0}:{1}", typeof (T).Name, ex.Message);

					return defaultValue;
				}
			}
		}

		private static void TryCreateConfig<T>(string fileName, T defaultValue)
		{
			if (!EnsureConfigDirectoryExists())
				return;

			var fileFullName = GetConfigFileFullName(fileName);

			if (File.Exists(fileFullName))
				return;

			if (defaultValue == null)
				return;

			var settings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				Indent = true
			};

			var xs = new XmlSerializer(typeof (T), (string) null);

			using (var fs = new FileStream(fileFullName, FileMode.Create))
			{
				using (var xw = XmlWriter.Create(fs, settings))
				{
					try
					{
						xs.Serialize(xw, defaultValue);
					}
					catch (Exception ex)
					{
						LocalLoggingService.Exception("Failed to serialize type {0}:{1}", typeof (T).Name, ex.Message);
					}
				}
			}
		}

		private static bool EnsureConfigDirectoryExists()
		{
			if (Directory.Exists(LocalConfigDirectory))
				return true;

			try
			{
				Directory.CreateDirectory(LocalConfigDirectory);

				return true;
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception("Failed to create directory {0}:{1}", LocalConfigDirectory, ex.Message);

				return false;
			}
		}

		private static string GetConfigFileFullName(string fileName)
		{
			return Path.Combine(LocalConfigDirectory, fileName);
		}
	}
}
