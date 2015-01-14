﻿namespace Hangerd.Test.Caching
{
	using Hangerd.Caching;
	using System;

	public class MyCacheProvider : Disposable, ICacheProvider
	{
		public T Get<T>(string key) { return default(T); }

		public void Set<T>(string key, T value, TimeSpan cacheTime) { }

		public void RemoveKey(string key) { }
	}
}
