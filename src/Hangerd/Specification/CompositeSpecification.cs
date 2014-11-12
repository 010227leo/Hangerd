namespace Hangerd.Specification
{
    /// <summary>
    /// Base class for composite specifications
    /// </summary>
	public abstract class CompositeSpecification<T> : Specification<T>
    {
        #region Properties

        /// <summary>
        /// Left side specification for this composite element
        /// </summary>
		public abstract ISpecification<T> LeftSideSpecification { get; }

        /// <summary>
        /// Right side specification for this composite element
        /// </summary>
		public abstract ISpecification<T> RightSideSpecification { get; }

        #endregion
    }
}
