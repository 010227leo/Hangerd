namespace Hangerd.Utility
{
	using Hangerd.Components;
	using Hangerd.Extensions;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Web;
	using System.Web.Compilation;

	public class BuildManagerWrapper
	{
		private static readonly BuildManagerWrapper _current = new BuildManagerWrapper();
		private IEnumerable<Assembly> _referencedAssemblies;
		private IEnumerable<Type> _publicTypes;
		private IEnumerable<Type> _concreteTypes;

		public static BuildManagerWrapper Current
		{
			get { return _current; }
		}

		public virtual IEnumerable<Assembly> Assemblies
		{
			get
			{
				if (HttpContext.Current == null)
				{
					var path = AppDomain.CurrentDomain.BaseDirectory;
					var allAssemblies = new List<Assembly>
					{
						Assembly.GetEntryAssembly()
					};

					foreach (var dll in Directory.GetFiles(path, "*.dll"))
					{
						try
						{
							allAssemblies.Add(Assembly.LoadFrom(dll));
						}
						catch (Exception ex)
						{
							LocalLoggingService.Exception(ex);
						}
					}

					return _referencedAssemblies ?? 
						(_referencedAssemblies = allAssemblies
							.Where(assembly => assembly != null && !assembly.GlobalAssemblyCache)
							.Distinct(new LambdaComparer<Assembly>((x, y) => x.FullName == y.FullName))
							.ToList());
				}
				else
				{
					return _referencedAssemblies ?? 
						(_referencedAssemblies = BuildManager.GetReferencedAssemblies()
							.Cast<Assembly>()
							.Where(assembly => !assembly.GlobalAssemblyCache)
							.ToList());
				}
			}
		}

		public IEnumerable<Type> PublicTypes
		{
			get
			{
				return _publicTypes ?? (_publicTypes = Assemblies.PublicTypes().ToList());
			}
		}

		public IEnumerable<Type> ConcreteTypes
		{
			get
			{
				return _concreteTypes ?? (_concreteTypes = Assemblies.ConcreteTypes().ToList());
			}
		}
	}
}
