using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Hotello.UI.Web.Helpers
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString StripHtml(this HtmlHelper helper, string text)
        {
            return new MvcHtmlString(Regex.Replace(text, @"<(.|\n)*?>", string.Empty));
        }
    }
}