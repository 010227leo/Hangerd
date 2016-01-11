using System;
using Hangerd.Domain.Repository;

namespace Hangerd.EntityFramework.Uow
{
    public static class RepositoryUowExtensions
    {
		public static TDbContext GetDbContext<TDbContext>(this IRepositoryContext unitOfWork) 
            where TDbContext : HangerdDbContext
        {
			if (!(unitOfWork is EfRepositoryContext))
				throw new ArgumentException("unitOfWork is not type of " + typeof(EfRepositoryContext).FullName, "unitOfWork");

			return ((EfRepositoryContext) unitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}