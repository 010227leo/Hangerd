namespace Hangerd.Mongodb
{
	using Hangerd.Bootstrapper;
	using Hangerd.Extensions;
	using Hangerd.Mongodb.Imp;
	using Microsoft.Practices.Unity;

	public class RegisterServiceTask : RegisterServiceBootstrapperTask
	{
		public RegisterServiceTask(IUnityContainer container) : base(container) { }

		public override int Order
		{
			get { return 1; }
		}

		public override void Execute()
		{
			container.RegisterTypeAsSingleton<IMongodbInsertService, MongodbInsertService>();
			container.RegisterTypeAsSingleton<IMongodbQueryService, MongodbQueryService>();
		}
	}
}
