using System;
using System.Linq.Expressions;

namespace Hangerd.Domain.Specification
{
	/// <summary>
	/// True specification
	/// </summary>
	public sealed class TrueSpecification<T> : Specification<T>
	{
		#region Specification overrides

		public override Expression<Func<T, bool>> SatisfiedBy()
		{
			return t => true;
		}

		#endregion
	}
}
