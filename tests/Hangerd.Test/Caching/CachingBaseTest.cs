namespace Hangerd.Test.Caching
{
	using Hangerd.Caching;
	using NUnit.Framework;
	using System;
	using System.Threading;

	public class CachingBaseTest : TestBase
	{
		[Test]
		public void GetTest()
		{
			var key = "GetTest.Key";
			var value = CacheManager.Get<string>(true, key, TimeSpan.FromSeconds(2), () => { return "value"; });

			Assert.AreEqual("value", value);
		}

		[Test]
		public void SetOrUpdateTest()
		{
			var key = "SetOrUpdateTest_2.Key";

			CacheManager.SetOrUpdate<string>(key, "value1", TimeSpan.FromSeconds(30));

			var value1 = CacheManager.Get<string>(true, key, TimeSpan.FromSeconds(30), () => { return null; });

			CacheManager.SetOrUpdate<string>(key, "value2", TimeSpan.FromSeconds(30));

			var value2 = CacheManager.Get<string>(true, key, TimeSpan.FromSeconds(30), () => { return null; });

			Assert.AreEqual("value1", value1);
			Assert.AreEqual("value2", value2);
		}

		[Test]
		public void RemoveKeyTest()
		{
			var key = "RemoveKeyTest.Key";
			var value = CacheManager.Get<string>(true, key, TimeSpan.FromSeconds(30), () => { return  "value" ; });

			CacheManager.RemoveKey(key);

			var value1 = CacheManager.Get<string>(key);

			Assert.AreEqual("value", value);
			Assert.IsNull(value1);
		}

		[Test]
		public void CacheExpiresTest()
		{
			var key = "CacheExpiresTest.Key";
			var value = CacheManager.Get<string>(true, key, TimeSpan.FromMilliseconds(300), () => { return "value"; });

			Thread.Sleep(500);

			var value1 = CacheManager.Get<string>(true, key, TimeSpan.FromSeconds(30), () => { return null; });

			Assert.AreEqual("value", value);
			Assert.IsNull(value1);
		}

	}
}
