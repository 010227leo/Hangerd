# Hangerd

[![Join the chat at https://gitter.im/010227leo/Hangerd](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/010227leo/Hangerd?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

[![Build status](https://ci.appveyor.com/api/projects/status/6lq99cxneooqhhk1/branch/master?svg=true)](https://ci.appveyor.com/project/010227leo/hangerd/branch/master)

A lightweight and efficient .NET based application development framework. It provides the fundamental libraries and tools for practicing and implementing Domain-Driven Design.

## Components

- Hangerd
- Hangerd.EntityFramework
- Hangerd.Mongodb
- Hangerd.Mvc

## Dependencies

- Unity v3.5.1404.0
- Newtonsoft.Json v6.0.1

## Usage

- **How to start**

```csharp
protected void Application_Start(object sender, EventArgs e)
{
	HangerdFramework.Start();
}

protected void Application_End()
{
	HangerdFramework.End();
}
```

- **Ioc**

```csharp
public class RegisterServiceTask : RegisterServiceBootstrapperTask
{
	public RegisterServiceTask(IUnityContainer container) : base(container) { }

	public override void Execute()
	{
		IocContainer.RegisterTypeAsSingleton<IMyService, MyService>();
	}
}

static void Main(string[] args)
{
	var myService = LocalServiceLocator.GetService<IMyService>();
}
```

- **Domain entity**

```csharp
public abstract class Account : EntityBase, IDeletable
{
	public string LoginName { get; private set; }

	public bool IsDeleted { get; set; }

	protected Account(string loginName)
	{
		if (string.IsNullOrWhiteSpace(loginName))
			throw new HangerdException("LoginName can not be null");

		LoginName = loginName;
	}
}
```

- **Domain event**

```csharp
public class AccountNameModifiedEvent : DomainEvent
{
	public AccountNameModifiedEvent(Account account)
		: base(account)
	{ }
}

public class AccountNameModifiedEventHandler : IDomainEventHandler<AccountNameModifiedEvent>
{
	public void Handle(AccountNameModifiedEvent e)
	{
		var account = e.Source as Account;

		account.LastModified = DateTime.Now;
	}
}

public class RegisterServiceTask : RegisterServiceBootstrapperTask
{
	public RegisterServiceTask(IUnityContainer container) : base(container) { }

	public override void Execute()
	{
		IocContainer.RegisterMultipleTypesAsPerResolve<IDomainEventHandler<AccountNameModifiedEvent>, AccountNameModifiedEventHandler>();
	}
}
	
public void ModifyName(string name)
{
	Name = name;

	DomainEvent.Publish(new AccountNameModifiedEvent(this));
}
```
