namespace Hangerd.Caching
{
	using Hangerd.Extensions;
	using System;
	using System.Runtime.Caching;

	public class DefaultCacheProvider : Disposable, ICacheProvider
	{
		private static readonly ObjectCache _cache = MemoryCache.Default;

		public T Get<T>(string key)
		{
			var value = _cache.Get(key);

			if (value != null)
			{
				return (T)value;
			}

			return default(T);
		}

		public void Set<T>(string key, T value, TimeSpan cacheTime)
		{
			if (value != null && !value.Equals(default(T)))
			{
				_cache.Set(key, value, DateTime.Now.Add(cacheTime));
			}
		}

		public void RemoveKey(string key)
		{
			if (_cache.Contains(key))
			{
				_cache.Remove(key);
			}
		}

		protected override void InternalDispose()
		{
			_cache.ParallelEach(t => _cache.Remove(t.Key));

			base.InternalDispose();
		}
	}
}
