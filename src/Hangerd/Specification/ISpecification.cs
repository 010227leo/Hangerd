using System;
using System.Linq.Expressions;

namespace Hangerd.Specification
{
	public interface ISpecification<T>
    {
        /// <summary>
        /// Check if this specification is satisfied by a specific expression lambda
        /// </summary>
		Expression<Func<T, bool>> SatisfiedBy();
    }
}
