namespace Hangerd.Mvc.ViewModels
{
	using System.Collections.Generic;

	public class PagedListModel<TDto>
	{
		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		public int TotalNumber { get; set; }

		public IEnumerable<TDto> List { get; set; }
	}
}