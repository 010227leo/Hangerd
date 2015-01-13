namespace Hangerd.Mvc.Extensions
{
	using Hangerd.Utility;
	using System.Web.Mvc;

	public static class HtmlExtention
	{
		public static MvcHtmlString IsSelected(this HtmlHelper helper, bool isSelected)
		{
			return isSelected ? new MvcHtmlString("selected='selected'") : null;
		}

		public static MvcHtmlString IsChecked(this HtmlHelper helper, bool isChecked)
		{
			return isChecked ? new MvcHtmlString("checked='checked'") : null;
		}

		public static MvcHtmlString IsVisible(this HtmlHelper helper, bool isVisible)
		{
			return !isVisible ? new MvcHtmlString("style='display:none;'") : null;
		}
	}
}
