namespace Hangerd.Entity
{
	using System;

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class RecordModifyAttribute : Attribute { }
}
