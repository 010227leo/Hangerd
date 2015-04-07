using System;
using System.Collections.Generic;

namespace Hangerd.MemoryQueue
{
	public class MemoryQueueServiceConfiguration<T>
	{
		private int _consumeThreadCount;
		private int _consumeItemCountInOneBatch;

		public string MemoryQueueName { get; set; }

		public Action<IList<T>> ConsumeAction { get; set; }

		public int ConsumeIntervalMilliseconds { get; set; }

		public int ConsumeThreadCount
		{
			get { return _consumeThreadCount; }
			set
			{
				if (value <= 0)
					throw new Exception("ConsumeThreadCount can not less than 0.");

				_consumeThreadCount = value;
			}
		}

		public int ConsumeItemCountInOneBatch
		{
			get { return _consumeItemCountInOneBatch; }
			set
			{
				if (value <= 0)
					throw new Exception("ConsumeItemCountInOneBatch can not less than 0.");

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
