using Hangerd.Components;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace Hangerd.Extensions
{
	public static class UnityContainerExtensions
	{
		public static IUnityContainer RegisterInstanceAsSingleton<TFrom>(this IUnityContainer instance, TFrom obj)
		{
			lock (instance)
			{
				return instance.RegisterInstance(obj, new ContainerControlledLifetimeManager());
			}
		}

		public static IUnityContainer RegisterInstanceAsPerResolve<TFrom>(this IUnityContainer instance, TFrom obj)
		{
			lock (instance)
			{
				return instance.RegisterInstance(obj, new PerResolveLifetimeManager());
			}
		}

		public static IUnityContainer RegisterTypeAsSingleton(this IUnityContainer instance, Type fromType, Type toType)
		{
			lock (instance)
			{
				return instance.RegisterType(fromType, toType, new ContainerControlledLifetimeManager());
			}
		}

		public static IUnityContainer RegisterTypeAsSingleton<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
		{
			lock (instance)
			{
				return instance.RegisterTypeAsSingleton(typeof(TFrom), typeof(TTo));
			}
		}

		public static IUnityContainer RegisterTypeAsPerResolve(this IUnityContainer instance, Type fromType, Type toType)
		{
			lock (instance)
			{
				return instance.RegisterType(fromType, toType, new PerResolveLifetimeManager());
			}
		}

		public static IUnityContainer RegisterTypeAsPerResolve<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
		{
			lock (instance)
			{
				return instance.RegisterTypeAsPerResolve(typeof(TFrom), typeof(TTo));
			}
		}

		public static IUnityContainer RegisterTypeAsPerRequest(this IUnityContainer instance, Type fromType, Type toType)
		{
			lock (instance)
			{
				return instance.RegisterType(fromType, toType, new PerRequestLifetimeManager());
			}
		}

		public static IUnityContainer RegisterTypeAsPerRequest<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
		{
			lock (instance)
			{
				return instance.RegisterTypeAsPerRequest(typeof(TFrom), typeof(TTo));
			}
		}

		#region Multiple

		public static IUnityContainer RegisterMultipleTypesAsSingleton(this IUnityContainer instance, Type fromType, Type toType)
		{
			lock (instance)
			{
				return instance.RegisterType(fromType, toType, toType.FullName, new ContainerControlledLifetimeManager());
			}
		}

		public static IUnityContainer RegisterMultipleTypesAsSingleton<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
		{
			lock (instance)
			{
				return instance.RegisterMultipleTypesAsSingleton(typeof(TFrom), typeof(TTo));
			}
		}

		public static IUnityContainer RegisterMultipleTypesAsTransient(this IUnityContainer instance, Type fromType, Type toType)
		{
			lock (instance)
			{
				return instance.RegisterType(fromType, toType, toType.FullName, new TransientLifetimeManager());
			}
		}

		public static IUnityContainer RegisterMultipleTypesAsTransient<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
		{
			lock (instance)
			{
				return instance.RegisterMultipleTypesAsTransient(typeof(TFrom), typeof(TTo));
			}
		}

		public static IUnityContainer RegisterMultipleTypesAsPerResolve(this IUnityContainer instance, Type fromType, Type toType)
		{
			lock (instance)
			{
				return instance.RegisterType(fromType, toType, toType.FullName, new PerResolveLifetimeManager());
			}
		}

		public static IUnityContainer RegisterMultipleTypesAsPerResolve<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
		{
			lock (instance)
			{
				return instance.RegisterMultipleTypesAsPerResolve(typeof(TFrom), typeof(TTo));
			}
		}

		public static IUnityContainer RegisterMultipleTypesAsPerRequest(this IUnityContainer instance, Type fromType, Type toType)
		{
			lock (instance)
			{
				return instance.RegisterType(fromType, toType, toType.FullName, new PerRequestLifetimeManager());
			}
		}

		public static IUnityContainer RegisterMultipleTypesAsPerRequest<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
		{
			lock (instance)
			{
				return instance.RegisterMultipleTypesAsPerRequest(typeof(TFrom), typeof(TTo));
			}
		}

		#endregion

		public static T ResolveOne<T>(this IUnityContainer instance)
		{
			return instance.ResolveAll<T>().FirstOrDefault();
		}
	}
}
