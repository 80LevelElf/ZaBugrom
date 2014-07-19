using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            IEnumerable<SelectListItem> items = values.Select(value => new SelectListItem()
            {
                Text = GetEnumDescription(value),
                Value = value.ToString(),
                Selected = (Equals(value, selectedValue))
            });
            return htmlHelper.DropDownListFor(expression, items);
        }

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}