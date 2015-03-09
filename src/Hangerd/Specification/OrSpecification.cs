using System;
using System.Linq.Expressions;

namespace Hangerd.Specification
{
	/// <summary>
	/// A Logic OR Specification
	/// </summary>
	public sealed class OrSpecification<T> : CompositeSpecification<T>
	{
		#region Members

		private readonly ISpecification<T> _rightSideSpecification;
		private readonly ISpecification<T> _leftSideSpecification;

		#endregion

		#region Public Constructor

		/// <summary>
		/// Default constructor for AndSpecification
		/// </summary>
		public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
		{
			if (leftSide == null)
				throw new ArgumentNullException("leftSide");

			if (rightSide == null)
				throw new ArgumentNullException("rightSide");

			_leftSideSpecification = leftSide;
			_rightSideSpecification = rightSide;
		}

		#endregion

		#region Composite Specification overrides

		/// <summary>
		/// Left side specification
		/// </summary>
		public override ISpecification<T> LeftSideSpecification
		{
			get { return _leftSideSpecification; }
		}

		/// <summary>
		/// Righ side specification
		/// </summary>
		public override ISpecification<T> RightSideSpecification
		{
			get { return _rightSideSpecification; }
		}

		public override Expression<Func<T, bool>> SatisfiedBy()
		{
			var left = _leftSideSpecification.SatisfiedBy();
			var right = _rightSideSpecification.SatisfiedBy();

			return (left.Or(right));
		}

		#endregion
	}
}
