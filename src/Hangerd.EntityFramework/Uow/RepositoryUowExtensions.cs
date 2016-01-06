using System;
using Hangerd.Domain.Repository;

namespace Hangerd.EntityFramework.Uow
{
    public static class RepositoryUowExtensions
    {
		public static TDbContext GetDbContext<TDbContext>(this IRepositoryUow unitOfWork) 
            where TDbContext : HangerdDbContext
        {
			if (!(unitOfWork is EfRepositoryUow))
				throw new ArgumentException("unitOfWork is not type of " + typeof(EfRepositoryUow).FullName, "unitOfWork");

			return ((EfRepositoryUow) unitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}