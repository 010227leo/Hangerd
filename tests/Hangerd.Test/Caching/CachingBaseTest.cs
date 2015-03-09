using Hangerd.Caching;
using Hangerd.Components;
using Hangerd.Extensions;
using NUnit.Framework;
using System;
using System.Threading;

namespace Hangerd.Test.Caching
{
	public class CachingBaseTest : TestBase
	{
		[Test]
		public void GetTest()
		{
			const string key = "GetTest.Key";

			var value = CacheManager.Get(true, key, TimeSpan.FromSeconds(2), () => "value");

			Assert.AreEqual("value", value);
		}

		[Test]
		public void SetOrUpdateTest()
		{
			const string key = "SetOrUpdateTest_2.Key";

			CacheManager.SetOrUpdate(key, "value1", TimeSpan.FromSeconds(30));

			var value1 = CacheManager.Get<string>(true, key, TimeSpan.FromSeconds(30), () => null);

			CacheManager.SetOrUpdate(key, "value2", TimeSpan.FromSeconds(30));

			var value2 = CacheManager.Get<string>(true, key, TimeSpan.FromSeconds(30), () => null);

			Assert.AreEqual("value1", value1);
			Assert.AreEqual("value2", value2);
		}

		[Test]
		public void RemoveKeyTest()
		{
			const string key = "RemoveKeyTest.Key";

			var value = CacheManager.Get(true, key, TimeSpan.FromSeconds(30), () => "value");

			CacheManager.RemoveKey(key);

			var value1 = CacheManager.Get<string>(key);

			Assert.AreEqual("value", value);
			Assert.IsNull(value1);
		}

		[Test]
		public void CacheExpiresTest()
		{
			const string key = "CacheExpiresTest.Key";

			var value = CacheManager.Get(true, key, TimeSpan.FromMilliseconds(300), () => "value");

			Thread.Sleep(500);

			var value1 = CacheManager.Get<string>(true, key, TimeSpan.FromSeconds(30), () => null);

			Assert.AreEqual("value", value);
			Assert.IsNull(value1);
		}

		[Test]
		public void CustomerCacheProviderTest()
		{
			HangerdFramework.Container.RegisterTypeAsSingleton<ICacheProvider, MyCacheProvider>();

			var myCacheProvider = LocalServiceLocator.GetService<ICacheProvider>();

			Assert.IsNotNull(myCacheProvider);
			Assert.AreEqual(typeof(MyCacheProvider), myCacheProvider.GetType());
		}
	}
}
