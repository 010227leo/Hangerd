using System;

namespace Hangerd.Validation
{
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class ValidatedNotNullAttribute : Attribute
	{
	}
}
