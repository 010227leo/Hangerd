﻿namespace Hangerd.Specification
{
	using System;
	using System.Linq.Expressions;

    /// <summary>
    /// A Direct Specification is a simple implementation
    /// of specification that acquire this from a lambda expression
    /// in  constructor
    /// </summary>
	public sealed class DirectSpecification<T> : Specification<T>
    {
        #region Members

		private Expression<Func<T, bool>> _matchingCriteria;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for Direct Specification
        /// </summary>
		public DirectSpecification(Expression<Func<T, bool>> matchingCriteria)
        {
			if (matchingCriteria == (Expression<Func<T, bool>>)null)
			{
				throw new ArgumentNullException("matchingCriteria");
			}

            _matchingCriteria = matchingCriteria;
        }

        #endregion

        #region Override

		public override Expression<Func<T, bool>> SatisfiedBy()
        {
            return _matchingCriteria;
        }

        #endregion
    }
}
