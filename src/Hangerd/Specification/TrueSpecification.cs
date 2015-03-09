using System;
using System.Linq.Expressions;

namespace Hangerd.Specification
{
	/// <summary>
	/// True specification
	/// </summary>
	public sealed class TrueSpecification<T> : Specification<T>
	{
		#region Specification overrides

		public override Expression<Func<T, bool>> SatisfiedBy()
		{
			const bool result = true;

			Expression<Func<T, bool>> trueExpression = t => result;

			return trueExpression;
		}

		#endregion
	}
}
