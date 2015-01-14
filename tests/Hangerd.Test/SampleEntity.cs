namespace Hangerd.Test
{
	using Hangerd.Entity;

	public sealed class SampleEntity : EntityBase, IValidatable, IDeletable
	{
		public string Name { get; set; }

		public int Order { get; set; }

		public bool IsDeleted { get; set; }

		public SampleEntity()
		{
			GenerateNewId();
		}

		public void Validate()
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				throw new HangerdException("Name is null or empty.");
			}
		}
	}
}
