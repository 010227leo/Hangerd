using System.ComponentModel.DataAnnotations;

namespace HangerdSample.Web.Models
{
	public class AccountRegisterModel
	{
		[Required(ErrorMessage = "登录账号不可为空")]
		public string LoginName { get; set; }

		[Required(ErrorMessage = "密码不可为空")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "两次密码输入不一致")]
		public string PasswordConfirm { get; set; }

		[Required(ErrorMessage = "姓名不可为空")]
		public string Name { get; set; }
	}
}
