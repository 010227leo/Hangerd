﻿namespace Hangerd.Specification
{
	using System;
	using System.Linq.Expressions;

    /// <summary>
    /// A logic AND Specification
    /// </summary>
	public sealed class AndSpecification<T> : CompositeSpecification<T>
    {
        #region Members

		private ISpecification<T> _rightSideSpecification = null;
		private ISpecification<T> _leftSideSpecification = null;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
		public AndSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
			if (leftSide == (ISpecification<T>)null)
			{
				throw new ArgumentNullException("leftSide");
			}

			if (rightSide == (ISpecification<T>)null)
			{
				throw new ArgumentNullException("rightSide");
			}

            this._leftSideSpecification = leftSide;
            this._rightSideSpecification = rightSide;
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
        /// Right side specification
        /// </summary>
		public override ISpecification<T> RightSideSpecification
        {
            get { return _rightSideSpecification; }
        }

		public override Expression<Func<T, bool>> SatisfiedBy()
        {
			Expression<Func<T, bool>> left = _leftSideSpecification.SatisfiedBy();
			Expression<Func<T, bool>> right = _rightSideSpecification.SatisfiedBy();

            return (left.And(right));
        }

        #endregion
    }
}
