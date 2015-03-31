using NUnit.Framework;

namespace Hangerd.Test.Entity
{
	public class EntityTest
	{
		[Test] 
		public void EntityEqualTest()
		{
			var e1 = new SampleEntity();
			var e2 = e1.Clone();

			Assert.AreEqual(e1, e2);
		}
	}
}
