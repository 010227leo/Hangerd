using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Hangerd.Entity;

namespace Hangerd.Test.Entity
{
	[Serializable]
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
				throw new HangerdException("Name is null or empty.");
		}

		public SampleEntity Clone()
		{
			var formatter = new BinaryFormatter();

			using (var stream = new MemoryStream())
			{
				formatter.Serialize(stream, this);
				stream.Seek(0, SeekOrigin.Begin);

				return (SampleEntity)formatter.Deserialize(stream);
			}
		}
	}
}
