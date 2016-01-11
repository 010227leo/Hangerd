using Hangerd;
using Hangerd.Domain.Entity;
using Hangerd.Event;
using Hangerd.Utility;
using Hangerd.Validation;
using HangerdSample.Domain.Events;

namespace HangerdSample.Domain.Models
{
	public sealed class Account : EntityBase, IDeletable
	{
		#region Public Properties

		/// <summary>
		/// 登录账号
		/// </summary>
		public string LoginName { get; private set; }

		/// <summary>
		/// 登录密码（加密后）
		/// </summary>
		public string EncryptedPassword { get; private set; }

		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// IDeletable
		/// </summary>
		public bool IsDeleted { get; set; }

		#endregion

		#region Constructors

		private Account()
		{
		}

		public Account(string loginName, string unencryptedPassword, string name)
		{
			if (string.IsNullOrWhiteSpace(loginName))
				throw new HangerdException("登录账号不可为空");

			if (!InputValidator.IsEmailAddress(loginName))
				throw new HangerdException("登录账号须为邮箱地址");

			LoginName = loginName;
			EncryptedPassword = GetEncryptedPassword(unencryptedPassword);
			Name = name;

			DomainEvent.Publish(new AccountCreatedEvent(this));
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// 验证密码
		/// </summary>
		public bool ValidatePassword(string unencryptedPassword)
		{
			var encryptedPassword = GetEncryptedPassword(unencryptedPassword);

			return encryptedPassword.Equals(EncryptedPassword);
		}

		/// <summary>
		/// 修改密码
		/// </summary>
		public void ChangePassword(string unencryptedPassword)
		{
			EncryptedPassword = GetEncryptedPassword(unencryptedPassword);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// 密码加密
		/// </summary>
		private static string GetEncryptedPassword(string unencryptedPassword)
		{
			if (string.IsNullOrWhiteSpace(unencryptedPassword))
				throw new HangerdException("密码不可为空");

			return CryptoHelper.GetMd5(unencryptedPassword);
		}

		#endregion
	}
}
