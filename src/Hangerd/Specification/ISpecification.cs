namespace Hangerd.Specification
{
	using System;
	using System.Linq.Expressions;

	public interface ISpecification<T>
    {
        /// <summary>
        /// Check if this specification is satisfied by a specific expression lambda
        /// </summary>
		Expression<Func<T, bool>> SatisfiedBy();
    }
}
