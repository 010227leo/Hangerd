using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Hangerd.Domain.Entity;

namespace Hangerd.Test.Domain.Entity
{
	[Serializable]
	public sealed class SampleEntity : EntityBase, IDeletable
	{
		public string Name { get; set; }

		public int Order { get; set; }

		public bool IsDeleted { get; set; }

		public SampleEntity()
		{
			GenerateNewId();
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
