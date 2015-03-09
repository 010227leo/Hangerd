using System;

namespace Hangerd.Caching
{
	public interface ICacheProvider : IDisposable
	{
		T Get<T>(string key);

		void Set<T>(string key, T value, TimeSpan cacheTime);

		void RemoveKey(string key);
	}
}
