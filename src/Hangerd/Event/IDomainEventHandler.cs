﻿namespace Hangerd.Event
{
	/// <summary>
	/// 表示继承于该接口的类型是领域事件处理器类型
	/// </summary>
	public interface IDomainEventHandler<in TDomainEvent> : IHandler<TDomainEvent>
		where TDomainEvent : class, IDomainEvent
	{ }
}
