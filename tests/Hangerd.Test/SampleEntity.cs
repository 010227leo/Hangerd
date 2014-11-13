namespace Hangerd.Test
{
	using Hangerd.Entity;

	internal class SampleEntity : EntityBase
	{
		public string Name { get; set; }

		public int Order { get; set; }

		public SampleEntity() 
		{
			this.GenerateNewId();
		}
	}
}
