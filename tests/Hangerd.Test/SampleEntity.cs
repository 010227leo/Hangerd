namespace Hangerd.Test
{
	using Hangerd.Entity;

	public class SampleEntity : EntityBase
	{
		public string Name { get; set; }

		public int Order { get; set; }

		public SampleEntity() 
		{
			this.GenerateNewId();
		}
	}
}
