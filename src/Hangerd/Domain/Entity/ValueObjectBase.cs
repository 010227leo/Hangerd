namespace Hangerd.Domain.Entity
{
	public class ValueObjectBase<T> where T : new()
	{
		protected ValueObjectBase()
		{
		}

		public static T Default
		{
			get { return new T(); }
		}
	}
}
