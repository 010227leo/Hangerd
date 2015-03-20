using Hangerd.Bootstrapper;
using Hangerd.Extensions;
using Hangerd.Mongodb.Imp;
using Microsoft.Practices.Unity;

namespace Hangerd.Mongodb
{
	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container) : base(container)
		{
		}

		public override int Order
		{
			get { return 1; }
		}

		public override void Execute()
		{
			_container.RegisterTypeAsSingleton<IMongodbInsertService, MongodbInsertService>();
			_container.RegisterTypeAsSingleton<IMongodbQueryService, MongodbQueryService>();
		}
	}
}
