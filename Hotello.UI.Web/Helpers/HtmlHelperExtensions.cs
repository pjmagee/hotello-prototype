using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Hotello.Common;
using PagedList;

namespace Hotello.UI.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString StripHtml(this HtmlHelper helper, string text)
        {
            return new MvcHtmlString(Regex.Replace(text, @"<(.|\n)*?>", string.Empty));
        }

        public static MvcHtmlString GetPagination<T>(this HtmlHelper helper, IPagedList<T> pagedList, string actionName)
        {
            const int limit = 10;
            int lastPage = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(pagedList.TotalItemCount) / Convert.ToDouble(limit)));
            int counter;
            const int adjacents = 2;
            int lpm1 = lastPage - 1;

            StringBuilder builder = new StringBuilder();
            builder.Append("<div class='pagination pagination-mini'>");
            builder.Append("<ul>");

            if (pagedList.HasPreviousPage)
            {
                builder.Append("<li>" + helper.ActionLink("<<", actionName, new { page = 1 }) + "</li>");
                builder.Append("<li>" + helper.ActionLink("<", actionName, new { page = pagedList.PageNumber - 1 }) + "</li>");
            }
            else
            {
                builder.Append("<li class='disabled'><span>&lt;</span></li>");
                builder.Append("<li class='disabled'><span>&lt;&lt;</span></li>");
            }
            if (lastPage < 7 + (adjacents * 2))
            {
                for (counter = 1; counter <= lastPage; counter++)
                {
                    if (counter == pagedList.PageNumber)
                    {
                        builder.Append("<li class='active'>" + helper.ActionLink(counter.ToString(), actionName, new {page = counter}) + "</li>");
                    }
                    else
                    {
                        builder.Append("<li>" + helper.ActionLink(counter.ToString(), actionName, new {page = counter}) + "</li>");
                    }
                }
            }
            else if (lastPage >= 7 + (adjacents * 2)) // enough to hide some
            {
                
                if (pagedList.PageNumber < 1 + (adjacents * 3))
                {
                    for (counter = 1; counter < 4 + (adjacents * 2); counter++)
                    {
                        if (counter == pagedList.PageNumber)
                        {
                            builder.Append("<li class='active'>" + helper.ActionLink(counter.ToString(), actionName, new {page = counter}) + "</li>");
                        }
                        else
                        {
                            builder.Append("<li>" + helper.ActionLink(counter.ToString(CultureInfo.InvariantCulture), actionName, new {page = counter}) + "</li>");
                        }
                    }

                    builder.Append("<li class='disabled'><span>...</span></li>");
                    builder.Append("<li>" + helper.ActionLink(lpm1.ToString(), actionName, new {page = lpm1.ToString() }) + "</li>");
                    builder.Append("<li>" + helper.ActionLink(lastPage.ToString(), actionName, new {page = lastPage }) + "</li>");
                }

                else if (lastPage - (adjacents * 2) > pagedList.PageNumber && pagedList.PageNumber > (adjacents * 2))
                {
                    builder.Append("<li>" + helper.ActionLink(1.ToString(), actionName, new { page = 1 }) + "</li>");
                    builder.Append("<li>" + helper.ActionLink(2.ToString(), actionName, new { page = 2 }) + "</li>");
                    
                    builder.Append("<li class='disabled'><span>...</span></li>");
               
                    for (counter = pagedList.PageNumber - adjacents; counter <= pagedList.PageNumber + adjacents; counter++)
                    {
                        if (counter == pagedList.PageNumber)
                        {
                            builder.Append("<li class='active'>" + helper.ActionLink(counter.ToString(), actionName, new {page = counter}) + "</li>");
                        }
                        else
                        {
                            builder.Append("<li>" + helper.ActionLink(counter.ToString(CultureInfo.InvariantCulture), actionName, new {page = counter}) + "</li>");
                        }
                    }
                    
                    builder.Append("<li class='disabled'><span>...</span></li>");

                    builder.Append("<li class='disabled'><span>...</span></li>");
                    builder.Append("<li>" + helper.ActionLink(lpm1.ToString(), actionName, new {page = lpm1.ToString() }) + "</li>");
                    builder.Append("<li>" + helper.ActionLink(lastPage.ToString(), actionName, new {page = lastPage }) + "</li>");
                }
                else
                {

                    builder.Append("<li>" + helper.ActionLink(1.ToString(), actionName, new { page = 1 }) + "</li>");
                    builder.Append("<li>" + helper.ActionLink(2.ToString(), actionName, new {page = 2 }) + "</li>");

                    
                    builder.Append("<li class='disabled'><span>...</span></li>");


                    for (counter = lastPage - (1 + (adjacents * 3)); counter <= lastPage; counter++)
                    {
                        if (counter == pagedList.PageNumber)
                        {
                            builder.Append("<li class='active'>" + helper.ActionLink(counter.ToString(), actionName, new { page = counter }) + "</li>");
                        }
                        else
                        {
                            builder.Append("<li>" + helper.ActionLink(counter.ToString(), actionName, new { page = counter }) + "</li>");
                        }
                    }
                }
                
                if (pagedList.HasNextPage)
                {
                    builder.Append("<li>" + helper.ActionLink(">", actionName, new { page = pagedList.PageNumber + 1 }) + "</li>");
                    builder.Append("<li>" + helper.ActionLink(">>", actionName, new { page = pagedList.PageCount }) + "</li>");
                }
                else
                {
                    builder.Append("<li class='disabled'><span>&lt;</span></li>");
                    builder.Append("<li class='disabled'><span>&lt;&lt;</span></li>");
                }
            }

            builder.Append("</ul>");
            builder.Append("</div>");
            

            return new MvcHtmlString(builder.ToString());
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
                
               

    }
}