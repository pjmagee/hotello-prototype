using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Hotello.Common;

namespace Hotello.UI.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString StripHtml(this HtmlHelper helper, string text)
        {
            return new MvcHtmlString(Regex.Replace(text, @"<(.|\n)*?>", string.Empty));
        }

        public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem
                {
                    Text = value.GetDescription(),
                    Value = value.ToString(),
                    Selected = (value.Equals(selectedValue))
                };

            return htmlHelper.DropDownList(name, items);
        }

        public static MvcHtmlString TryPartial(this HtmlHelper helper, string viewName, object model)
        {
            try
            {
                return helper.Partial(viewName, model);
            }
            catch (Exception)
            {

            }
            return MvcHtmlString.Empty;
        }
    }
}