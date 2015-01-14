﻿namespace Hangerd.Entity
{
	public class ModifiedProperty
	{
		public object OldValue { get; set; }

		public object NewValue { get; set; }

		public ModifiedProperty(object oldValue, object newValue)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}
	}
}
