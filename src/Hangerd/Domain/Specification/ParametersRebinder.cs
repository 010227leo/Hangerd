using System.Collections.Generic;
using System.Linq.Expressions;

namespace Hangerd.Domain.Specification
{
	/// <summary>
	/// Helper for rebinder parameters without use Invoke method in expressions 
	/// ( this methods is not supported in all linq query providers, 
	/// for example in Linq2Entities is not supported)
	/// </summary>
	public sealed class ParameterRebinder : ExpressionVisitor
	{
		private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

		/// <summary>
		/// Default construcotr
		/// </summary>
		public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
		{
			_map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
		}

		/// <summary>
		/// Replate parameters in expression with a Map information
		/// </summary>
		public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
		{
			return new ParameterRebinder(map).Visit(exp);
		}

		/// <summary>
		/// Visit pattern method
		/// </summary>
		protected override Expression VisitParameter(ParameterExpression p)
		{
			ParameterExpression replacement;

			if (_map.TryGetValue(p, out replacement))
				p = replacement;

			return base.VisitParameter(p);
		}
	}
}
