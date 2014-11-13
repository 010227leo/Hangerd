namespace Hangerd.MemoryQueue
{
	using System;
	using System.Collections.Generic;

	public interface IMemoryQueueService<T> : IDisposable
    {
		/// <summary>
		/// 初始化队列服务
		/// </summary>
		void Init(MemoryQueueServiceConfiguration<T> configuration);

		/// <summary>
		/// 入列一条记录
		/// </summary>
		void Enqueue(T item);

		/// <summary>
		/// 入列多条记录
		/// </summary>
		void EnqueueBatch(IList<T> item);

		/// <summary>
		/// 消费数量
		/// </summary>
		long TotalConsumeItemCount { get; }
    }
}
