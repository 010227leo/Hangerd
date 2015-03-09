using System;
using Hangerd.Components;

namespace Hangerd.Caching
{
	public static class CacheManager
	{
		private static readonly Lazy<ICacheProvider> _cacheProvider = new Lazy<ICacheProvider>(LocalServiceLocator.GetService<ICacheProvider>);

		private static ICacheProvider CacheProvider
		{
			get { return _cacheProvider.Value; }
		}

		public static TEntity Get<TEntity>(bool fromCache, string key, TimeSpan cacheTime, Func<TEntity> getItem)
		{
			var value = fromCache ? CacheProvider.Get<TEntity>(key) : default(TEntity);

			if (value != null && !value.Equals(default(TEntity)))
				return value;

			value = getItem();

			if (fromCache)
				CacheProvider.Set(key, value, cacheTime);

			return value;
		}

		public static TEntity Get<TEntity>(string key)
		{
			return CacheProvider.Get<TEntity>(key);
		}

		public static void SetOrUpdate<TEntity>(string key, TEntity value, TimeSpan cacheTime)
		{
			CacheProvider.Set(key, value, cacheTime);
		}

		public static void RemoveKey(string key)
		{
			CacheProvider.RemoveKey(key);
		}
	}
}
