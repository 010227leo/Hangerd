namespace Hangerd.Components
{
	using Microsoft.Practices.Unity;
	using System;
	using System.Runtime.Remoting.Messaging;
	using System.Web;

	internal class PerRequestLifetimeManager : LifetimeManager
	{
		private Guid _key;

		public PerRequestLifetimeManager() 
		{
			_key = Guid.NewGuid();
		}

		#region ILifetimeManager Members

		public override object GetValue()
		{
			object result = null;

			if (HttpContext.Current != null)
			{
				//HttpContext avaiable ( ASP.NET ..)
				if (HttpContext.Current.Items[_key.ToString()] != null)
				{
					result = HttpContext.Current.Items[_key.ToString()];
				}
			}
			else
			{
				//Not in ASP.NET Environment, UnitTesting, WinForms, WPF etc.
				result = CallContext.GetData(_key.ToString());
			}

			return result;
		}

		public override void RemoveValue()
		{
			if (HttpContext.Current != null)
			{
				//HttpContext avaiable ( ASP.NET ..)
				if (HttpContext.Current.Items.Contains(_key.ToString()) && HttpContext.Current.Items[_key.ToString()] != null)
				{
					HttpContext.Current.Items[_key.ToString()] = null;
				}
			}
			else
			{
				//Not in ASP.NET Environment, UnitTesting, WinForms, WPF etc.
				CallContext.FreeNamedDataSlot(_key.ToString());
			}
		}

		public override void SetValue(object newValue)
		{
			if (HttpContext.Current != null)
			{
				//HttpContext avaiable ( ASP.NET ..)
				if (HttpContext.Current.Items[_key.ToString()] == null)
				{
					HttpContext.Current.Items[_key.ToString()] = newValue;
				}
			}
			else
			{
				//Not in ASP.NET Environment, UnitTesting, WinForms, WPF etc.
				CallContext.SetData(_key.ToString(), newValue);
			}
		}

		#endregion
	}
}
