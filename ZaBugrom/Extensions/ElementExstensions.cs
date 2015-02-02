using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ZaBugrom.Extensions
{
    public static class ElementExstensions
    {
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TEnum>> expression)
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<Enum>();
            var selectedValue = (expression.Compile().Invoke(htmlHelper.ViewData.Model) as Enum);
            IEnumerable<SelectListItem> items = values.Select(value => new SelectListItem
            {
                Text = EnumExtension.GetEnumDescription(value),
                Value = value.ToString(),
                Selected = (Equals(value, selectedValue))
            });
            return htmlHelper.DropDownListFor(expression, items);
        }

        public static MvcHtmlString VideoTag<TModel>(this HtmlHelper<TModel> htmlHelper, string videoId)
        {
            return new MvcHtmlString(string.Format("[video src=\"{0}\"]", videoId));
        }
    }
}