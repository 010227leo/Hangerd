using Hangerd.Utility.Generator;
using System;
using System.Collections.Generic;

namespace Hangerd.Entity
{
	public abstract class EntityBase
	{
		private readonly Dictionary<string, ModifiedProperty> _modifiedPropertitiesRecords = new Dictionary<string, ModifiedProperty>();
		private int? _requestedHashCode;

		public string Id { get; private set; }

		public DateTime LastModified { get; set; }

		public Dictionary<string, ModifiedProperty> ModifiedPropertiesRecords
		{
			get { return _modifiedPropertitiesRecords; }
		}

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

		public void RecordModifiedProperty(string propertyName, object oldValue, object newValue)
		{
			if (!_modifiedPropertitiesRecords.ContainsKey(propertyName))
				_modifiedPropertitiesRecords.Add(propertyName, new ModifiedProperty(oldValue, newValue));
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
			if (IsTransient())
				return base.GetHashCode();

			if (!_requestedHashCode.HasValue)
				_requestedHashCode = Id.GetHashCode() ^ 31;

			return _requestedHashCode.Value;
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
