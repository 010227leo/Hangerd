using System;
using Hangerd.Components;

namespace Hangerd
{
	public abstract class HangerdServiceBase
	{
		protected static HangerdResult<TResult> TryReturn<TResult>(Func<TResult> operate, string successMessage = null)
		{
			try
			{
				return new HangerdResult<TResult>(operate(), successMessage ?? "操作成功");
			}
			catch (HangerdException ex)
			{
				return new HangerdResult<TResult>(default(TResult), ex.Message);
			}
			catch (Exception ex)
			{
				LocalLoggingService.Exception(ex);

				return new HangerdResult<TResult>(default(TResult), "系统异常,请在日志中查看详情");
			}
		}

		protected static HangerdResult<bool> TryOperate(Action operate, string successMessage = null)
		{
			return TryReturn(() =>
			{
				operate();

				return true;
			}, successMessage);
		}
	}
}
