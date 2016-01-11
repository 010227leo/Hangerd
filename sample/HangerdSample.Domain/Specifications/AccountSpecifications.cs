using Hangerd.Domain.Specification;
using HangerdSample.Domain.Models;

namespace HangerdSample.Domain.Specifications
{
	public class AccountSpecifications : EntitySpecifications<Account>
	{
		public static Specification<Account> LoginNameEquals(string loginName)
		{
			return new DirectSpecification<Account>(a => a.LoginName == loginName);
		}
	}
}
