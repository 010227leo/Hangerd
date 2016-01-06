using System.Runtime.Remoting.Messaging;
using Hangerd.Components;

namespace Hangerd.Uow
{
	public class CallContextCurrentUowProvider : ICurrentUowProvider
    {
		public TUnitOfWork GetCurrent<TUnitOfWork>() where TUnitOfWork : class, IUnitOfWork
		{
			var uowKey = GetUowKey<TUnitOfWork>();

			return CallContext.GetData(uowKey) as TUnitOfWork;
		}

		public void SetCurrent<TUnitOfWork>(TUnitOfWork value) where TUnitOfWork : class, IUnitOfWork
        {
			var uowKey = GetUowKey<TUnitOfWork>();

            if (value == null)
            {
				LocalLoggingService.Debug("FreeNamedDataSlot:" + uowKey);
				CallContext.FreeNamedDataSlot(uowKey);
                return;
            }

			CallContext.SetData(uowKey, value);
        }

		private static string GetUowKey<TUnitOfWork>()
		{
			return string.Concat("Hangerd.Uow.Current", typeof(TUnitOfWork).Name);
		}
    }
}