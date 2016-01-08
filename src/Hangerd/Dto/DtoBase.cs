using System;

namespace Hangerd.Dto
{
	public class DtoBase
	{
		public string Id { get; set; }

		public DateTime CreateTime { get; set; }

		public DateTime LastModified { get; set; }
	}
}
