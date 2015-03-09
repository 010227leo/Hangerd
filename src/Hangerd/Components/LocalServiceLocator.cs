using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace Hangerd.Components
{
	public static class LocalServiceLocator
	{
		public static T GetService<T>()
		{
			try
			{
				return HangerdFramework.Container.Resolve<T>();
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception("LocalServiceLocator Resolve error '{0}', Message：{1}", typeof(T).FullName, ex.ToString());

				return default(T);
			}
		}

		public static IEnumerable<T> GetServices<T>()
		{
			try
			{
				return HangerdFramework.Container.ResolveAll<T>();
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception("LocalServiceLocator ResolveAll error '{0}', Message：{1}", typeof(T).FullName, ex.ToString());

				return default(IEnumerable<T>);
			}
		}
	}
}
