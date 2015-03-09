using System;
using System.Linq.Expressions;

namespace Hangerd.Specification
{
	/// <summary>
    /// Represent a Expression Specification
    /// </summary>
	public abstract class Specification<T> : ISpecification<T>
    {
        #region ISpecification<TEntity> Members

        /// <summary>
        /// IsSatisFied Specification pattern method,
        /// </summary>
		public abstract Expression<Func<T, bool>> SatisfiedBy();

        #endregion

        #region Override Operators

        /// <summary>
        ///  And operator
        /// </summary>
		public static Specification<T> operator &(Specification<T> leftSideSpecification, Specification<T> rightSideSpecification)
        {
            return new AndSpecification<T>(leftSideSpecification, rightSideSpecification);
        }
       
        /// <summary>
        /// Or operator
        /// </summary>
		public static Specification<T> operator |(Specification<T> leftSideSpecification, Specification<T> rightSideSpecification)
        {
            return new OrSpecification<T>(leftSideSpecification, rightSideSpecification);
        }
        
        /// <summary>
        /// Not specification
        /// </summary>
		public static Specification<T> operator !(Specification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }
      
        /// <summary>
        /// Override operator false, only for support AND OR operators
        /// </summary>
		public static bool operator false(Specification<T> specification)
        {
            return false;
        }
        
        /// <summary>
        /// Override operator True, only for support AND OR operators
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "specification")]
		public static bool operator true(Specification<T> specification)
        {
			//return false for || operator
			return false;
        }
      
        #endregion
    }
}

