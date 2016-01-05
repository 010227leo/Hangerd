using System;
using Hangerd.Utility.Generator;

namespace Hangerd.Domain.Entity
{
	[Serializable]
	public abstract class EntityBase
	{
		public string Id { get; protected set; }

		public DateTime LastModified { get; set; }

		protected EntityBase()
		{
			LastModified = DateTime.Now;
		}

		public bool IsTransient()
		{
			return string.IsNullOrWhiteSpace(Id) || Id == Guid.Empty.ToString("N");
		}

		public virtual void GenerateNewId()
		{
			if (IsTransient())
				Id = IdentityGenerator.NewSequentialGuid().ToString("N");
		}

		public override bool Equals(object obj)
		{
			if (!(obj is EntityBase))
				return false;

			if (ReferenceEquals(this, obj))
				return true;

			var item = (EntityBase) obj;

			if (item.IsTransient() || IsTransient())
				return false;

			return item.Id == Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ 31;
		}

		public static bool operator ==(EntityBase left, EntityBase right)
		{
			return Object.Equals(left, null) ? Object.Equals(right, null) : left.Equals(right);
		}

		public static bool operator !=(EntityBase left, EntityBase right)
		{
			return !(left == right);
		}
	}
}
