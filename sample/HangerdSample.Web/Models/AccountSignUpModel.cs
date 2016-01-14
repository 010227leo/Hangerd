using System.ComponentModel.DataAnnotations;

namespace HangerdSample.Web.Models
{
	public class AccountSignUpModel
	{
		[Required(ErrorMessage = "Email is empty")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is empty")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Different passwords")]
		public string PasswordConfirm { get; set; }

		[Required(ErrorMessage = "Name is empty")]
		public string Name { get; set; }
	}
}
