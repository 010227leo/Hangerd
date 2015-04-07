using System;
using Hangerd.Caching;
using Hangerd.Components;
using Hangerd.Extensions;
using NUnit.Framework;

namespace Hangerd.Test.Caching
{
	public class CustomerCacheProvider : Disposable, ICacheProvider
	{
		public T Get<T>(string key)
		{
			throw new NotImplementedException("CustomerCacheProvider.Get not implemented");
		}

		public void Set<T>(string key, T value, TimeSpan cacheTime)
		{
			throw new NotImplementedException("CustomerCacheProvider.Set not implemented");
		}

		public void RemoveKey(string key)
		{
			throw new NotImplementedException("CustomerCacheProvider.RemoveKey not implemented");
		}
	}

	public class CustomerCacheProviderTest : TestBase
	{
		[Test]
		public void ChangeProviderTest()
		{
			HangerdFramework.Container.RegisterTypeAsSingleton<ICacheProvider, CustomerCacheProvider>();

			var customerCacheProvider = LocalServiceLocator.GetService<ICacheProvider>();

			Assert.IsNotNull(customerCacheProvider);
			Assert.AreEqual(typeof (CustomerCacheProvider), customerCacheProvider.GetType());

			try
			{
				const string key = "CustomerCacheProviderTest.Key";

				CacheManager.SetOrUpdate(key, string.Empty, TimeSpan.FromDays(1));

				Assert.Fail("Method should be not implemented.");
			}
			catch (NotImplementedException ex)
			{
				Assert.Pass(ex.Message);
			}
		}
	}
}
