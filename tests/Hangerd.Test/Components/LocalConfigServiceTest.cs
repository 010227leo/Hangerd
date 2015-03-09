using Hangerd.Components;
using NUnit.Framework;

namespace Hangerd.Test.Components
{
	public class LocalConfigServiceTest
	{
		public class TestConfig
		{
			public string Key { get; set; }

			public int CacheMinutes { get; set; }
		}

		[Test]
		public void GetConfigTest()
		{
			var config = LocalConfigService.GetConfig(new TestConfig
			{
				Key = "010227"
			});

			Assert.AreEqual("010227", config.Key);

			var config2 = LocalConfigService.GetConfig<TestConfig>(null);

			Assert.AreEqual("010227", config2.Key);
		}
	}
}
