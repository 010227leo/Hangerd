using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hangerd.Specification
{
	/// <summary>
	/// NotEspecification convert a original specification with NOT logic operator
	/// </summary>
	public sealed class NotSpecification<T> : Specification<T>
	{
		#region Members

		private readonly Expression<Func<T, bool>> _originalCriteria;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for NotSpecificaiton
		/// </summary>
		public NotSpecification(ISpecification<T> originalSpecification)
		{
			if (originalSpecification == null)
				throw new ArgumentNullException("originalSpecification");

			_originalCriteria = originalSpecification.SatisfiedBy();
		}

		/// <summary>
		/// Constructor for NotSpecification
		/// </summary>
		public NotSpecification(Expression<Func<T, bool>> originalSpecification)
		{
			if (originalSpecification == null)
				throw new ArgumentNullException("originalSpecification");

			_originalCriteria = originalSpecification;
		}

		#endregion

		#region Override Specification methods

		public override Expression<Func<T, bool>> SatisfiedBy()
		{
			return Expression.Lambda<Func<T, bool>>(Expression.Not(_originalCriteria.Body),
				_originalCriteria.Parameters.Single());
		}

		#endregion
	}
}
