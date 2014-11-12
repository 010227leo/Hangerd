namespace Hangerd.Entity
{
	using Hangerd.Utility.Generator;
	using System;
	using System.Collections.Generic;

	public abstract class EntityBase
	{
		private Dictionary<string, string> _modifiedPropertitiesRecords = new Dictionary<string, string>();
		private int? _requestedHashCode;

		public virtual string Id { get; set; }

		public virtual DateTime LastModified { get; set; }

		public EntityBase()
		{
			LastModified = DateTime.Now;
		}

		public bool IsTransient()
		{
			return string.IsNullOrWhiteSpace(this.Id) || this.Id == Guid.Empty.ToString("N");
		}

		public virtual void GenerateNewId()
		{
			if (this.IsTransient())
			{
				this.Id = IdentityGenerator.NewSequentialGuid().ToString("N");
			}
		}

		public Dictionary<string, string> GetModifiedPropertiesRecords()
		{
			return _modifiedPropertitiesRecords;
		}

		public void CleanModifiedPropertiesRecords()
		{
			_modifiedPropertitiesRecords.Clear();
		}

		public void RecordModifiedProperty(string propertyName, object oldValue, object newValue)
		{
			if (!_modifiedPropertitiesRecords.ContainsKey(propertyName))
			{
				_modifiedPropertitiesRecords.Add(propertyName, string.Format("old:'{0}',new:'{1}'", oldValue, newValue));
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is EntityBase))
			{
				return false;
			}

			if (Object.ReferenceEquals(this, obj))
			{
				return true;
			}

			var item = obj as EntityBase;

			if (item.IsTransient() || this.IsTransient())
			{
				return false;
			}
			else
			{
				return item.Id == this.Id;
			}
		}

		public override int GetHashCode()
		{
			if (!IsTransient())
			{
				if (!_requestedHashCode.HasValue)
				{
					_requestedHashCode = this.Id.GetHashCode() ^ 31;
				}

				return _requestedHashCode.Value;
			}
			else
			{
				return base.GetHashCode();
			}
		}

		public static bool operator ==(EntityBase left, EntityBase right)
		{
			if (Object.Equals(left, null))
			{
				return (Object.Equals(right, null)) ? true : false;
			}
			else
			{
				return left.Equals(right);
			}
		}

		public static bool operator !=(EntityBase left, EntityBase right)
		{
			return !(left == right);
		}
	}
}
