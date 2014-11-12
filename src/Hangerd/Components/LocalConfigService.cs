﻿namespace Hangerd.Components
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Xml;
	using System.Xml.Serialization;

	public class LocalConfigService
	{
		private static readonly string _localConfigDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
		private static readonly Dictionary<string, object> _configCache = new Dictionary<string, object>();
		private static readonly object _locker = new object();

		public static T GetConfig<T>(T defaultValue)
		{
			var fileName = string.Format("{0}.config", typeof(T).Name);

			return GetConfig(fileName, defaultValue);
		}

		public static T GetConfig<T>(string fileName, T defaultValue)
		{
			object instance = null;
			var fileFullName = GetConfigFileFullName(fileName);

			if (_configCache.TryGetValue(fileFullName, out instance))
			{
				return (T)instance;
			}

			lock (_locker)
			{
				if (_configCache.TryGetValue(fileFullName, out instance))
				{
					return (T)instance;
				}

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
					LocalLoggingService.Exception("加载配置文件 {0} 失败！异常信息：{1}", fileFullName, ex.Message);

					return defaultValue;
				}

				var xmlSerializer = new XmlSerializer(typeof(T));

				using (var sr = new StringReader(doc.OuterXml))
				{
					try
					{
						instance = (T)xmlSerializer.Deserialize(sr);

						_configCache.Add(fileFullName, instance);

						return (T)instance;
					}
					catch (Exception ex)
					{
						LocalLoggingService.Exception("反序列化异常，类型名称：{0}，异常信息：{1}", typeof(T).Name, ex.Message);

						return defaultValue;
					}
				}
			}
		}

		private static void TryCreateConfig<T>(string fileName, T defaultValue)
		{
			if (!EnsureConfigDirectoryExists())
			{
				return;
			}

			var fileFullName = GetConfigFileFullName(fileName);

			if (File.Exists(fileFullName))
			{
				return;
			}

			if (defaultValue == null)
			{
				return;
			}

			var settings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				Indent = true
			};

			var xs = new XmlSerializer(typeof(T), (string)null);

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
						LocalLoggingService.Exception("序列化异常，类型名称：{0}，异常信息：{1}", typeof(T).Name, ex.Message);
					}
				}
			}
		}

		private static bool EnsureConfigDirectoryExists()
		{
			if (Directory.Exists(_localConfigDirectory))
			{
				return true;
			}

			try
			{
				Directory.CreateDirectory(_localConfigDirectory);

				return true;
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception("创建目录 {0} 失败！异常信息：{1}", _localConfigDirectory, ex.Message);

				return false;
			}
		}

		private static string GetConfigFileFullName(string fielName)
		{
			return Path.Combine(_localConfigDirectory, fielName);
		}
	}
}
