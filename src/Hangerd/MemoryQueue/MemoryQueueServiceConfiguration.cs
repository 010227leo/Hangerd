using System;
using System.Collections.Generic;

namespace Hangerd.MemoryQueue
{
	public class MemoryQueueServiceConfiguration<T>
	{
		public string MemoryQueueName { get; set; }

		public Action<IList<T>> ConsumeAction { get; set; }

		public int ConsumeIntervalMilliseconds { get; set; }

		private int _consumeThreadCount;

		public int ConsumeThreadCount
		{
			get
			{
				return _consumeThreadCount;
			}
			set
			{
				if (value <= 0)
					throw new ArgumentException("Invalid argument!", "ConsumeThreadCount");

				_consumeThreadCount = value;
			}
		}

		private int _consumeItemCountInOneBatch;

		public int ConsumeItemCountInOneBatch
		{
			get
			{
				return _consumeItemCountInOneBatch;
			}
			set
			{
				if (value <= 0)
					throw new ArgumentException("Invalid argument!", "ConsumeItemCountInOneBatch");

				_consumeItemCountInOneBatch = value;
			}
		}

		public MemoryQueueServiceConfiguration(string queueName, Action<IList<T>> consumeAction)
		{
			MemoryQueueName = queueName;
			ConsumeAction = consumeAction;
			ConsumeThreadCount = 1;
			ConsumeIntervalMilliseconds = 100;
			ConsumeItemCountInOneBatch = 10;
		}
	}
}
