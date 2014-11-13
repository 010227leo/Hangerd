namespace Hangerd.MemoryQueue
{
	using System;
	using System.Collections.Generic;

	public class MemoryQueueServiceConfiguration<T>
	{
		/// <summary>
		/// 内存队列名
		/// </summary>
		public string MemoryQueueName { get; set; }

		/// <summary>
		/// 消费数据的委托
		/// </summary>
		public Action<IList<T>> ConsumeAction { get; set; }

		/// <summary>
		/// 消费数据的时间间隔毫秒
		/// </summary>
		public int ConsumeIntervalMilliseconds { get; set; }

		private int _consumeThreadCount;

		/// <summary>
		/// 消费的线程总数
		/// </summary>
		public int ConsumeThreadCount
		{
			get
			{
				return _consumeThreadCount;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Invalid argument!", "ConsumeThreadCount");
				}

				_consumeThreadCount = value;
			}
		}

		private int _consumeItemCountInOneBatch;

		/// <summary>
		/// 消费数据的批量项数
		/// </summary>
		public int ConsumeItemCountInOneBatch
		{
			get
			{
				return _consumeItemCountInOneBatch;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Invalid argument!", "ConsumeItemCountInOneBatch");
				}

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
