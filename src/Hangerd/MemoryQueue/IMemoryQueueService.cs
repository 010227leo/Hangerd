namespace Hangerd.MemoryQueue
{
	using System;
	using System.Collections.Generic;

	public interface IMemoryQueueService<T> : IDisposable
    {
		/// <summary>
		/// 初始化队列服务
		/// </summary>
		/// <param name="configuration"></param>
		void Init(MemoryQueueServiceConfiguration<T> configuration);

		/// <summary>
		/// 入列一条记录
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		void Enqueue(T item);

		/// <summary>
		/// 入列多条记录
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		void EnqueueBatch(IList<T> item);
    }
}
