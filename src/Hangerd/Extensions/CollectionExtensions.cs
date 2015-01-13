namespace Hangerd.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public static class CollectionExtensions
	{
		public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
		{
			if (instance == null)
			{
				return;
			}

			foreach (var item in instance)
			{
				action(item);
			}
		}

		public static void ParallelEach<T>(this IEnumerable<T> instance, Action<T> action)
		{
			if (instance != null)
			{
				Parallel.ForEach(instance, action);
			}
		}

		/// <summary>
		/// if pageIndex is 0 and pageSize is 0, return all
		/// </summary>
		public static IQueryable<T> Paging<T>(this IOrderedQueryable<T> instance, int pageIndex, int pageSize, out int totalNumber)
		{
			totalNumber = 0;

			if (instance != null)
			{
				totalNumber = instance.Count();

				return instance.Paging(pageIndex, pageSize);
			}

			return null;
		}

		/// <summary>
		/// if pageIndex is 0 and pageSize is 0, return all
		/// </summary>
		public static IQueryable<T> Paging<T>(this IOrderedQueryable<T> instance, int pageIndex, int pageSize)
		{
			if (instance != null)
			{
				if (pageIndex == 0 && pageSize == 0)
				{
					return instance;
				}

				return instance.Skip((pageIndex - 1) * pageSize).Take(pageSize);
			}

			return null;
		}
	}
}
