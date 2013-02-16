using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Hotello.UI.Web.Helpers
{
    public static class ControlGroupExtensions
    {
        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, Expression<Func<T, object>> modelProperty)
        {
            var controlGroupWrapper = new TagBuilder("div");
            controlGroupWrapper.AddCssClass("control-group");
            var partialFieldName = ExpressionHelper.GetExpressionText(modelProperty);
            var fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(partialFieldName);
            if (!html.ViewData.ModelState.IsValidField(fullHtmlFieldName))
            {
                controlGroupWrapper.AddCssClass("error");
            }
            var openingTag = controlGroupWrapper.ToString(TagRenderMode.StartTag);
            return MvcHtmlString.Create(openingTag);
        }

        public static IHtmlString BeginControlGroupFor<T>(this HtmlHelper<T> html, string propertyName)
        {
            var controlGroupWrapper = new TagBuilder("div");
            controlGroupWrapper.AddCssClass("control-group");
            var partialFieldName = propertyName;
            var fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(partialFieldName);
            if (!html.ViewData.ModelState.IsValidField(fullHtmlFieldName))
            {
                controlGroupWrapper.AddCssClass("error");
            }
            var openingTag = controlGroupWrapper.ToString(TagRenderMode.StartTag);
            return MvcHtmlString.Create(openingTag);
        }

        public static IHtmlString EndControlGroup(this HtmlHelper html)
        {
            return MvcHtmlString.Create("</div>");
        }
    }
}