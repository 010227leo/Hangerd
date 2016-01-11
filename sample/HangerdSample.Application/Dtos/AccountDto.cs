using Hangerd.Dto;

namespace HangerdSample.Application.Dtos
{
	public class AccountDto : DtoBase
	{
		/// <summary>
		/// 登录账号
		/// </summary>
		public string LoginName { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }
	}
}
