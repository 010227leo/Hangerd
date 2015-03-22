using Hangerd.Components;
using Hangerd.Entity;
using System;

namespace Hangerd.Repository
{
	public static class RepositoryFactory
	{
		/// <summary>
		/// 从UnityContainer获取仓储实例，非泛型尽量使用构造函数注入
		/// </summary>
		/// <exception cref="System.NullReferenceException">repository is null</exception>
		public static TRepository GetRepository<TRepository, TEntity>()
			where TRepository : IRepository<TEntity>
			where TEntity : EntityBase
		{
			var repository = LocalServiceLocator.GetService<TRepository>();

			if (repository == null)
				throw new NullReferenceException(
					string.Format("未获取到仓储实例, type:{0}", typeof(TRepository).FullName));

			return repository;
		}
	}
}
