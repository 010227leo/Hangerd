using System;
using System.Collections.Generic;

namespace Hangerd.MemoryQueue
{
	public interface IMemoryQueueService<T> : IDisposable
    {
		void Init(MemoryQueueServiceConfiguration<T> configuration);

		void Enqueue(T item);

		void EnqueueBatch(IList<T> item);

		long TotalConsumeItemCount { get; }
    }
}
