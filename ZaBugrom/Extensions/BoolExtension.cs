using System.Web.Mvc;

namespace ZaBugrom.Extensions
{
    public static class BoolExtension
    {
        public static MvcHtmlString GetMessageIsReadClass(bool isRead)
        {
            if (isRead)
                return new MvcHtmlString("readed");

            return new MvcHtmlString("not-readed");
        }

        public static MvcHtmlString GetMessageIsReadTextClass(bool isRead)
        {
            if (isRead)
                return new MvcHtmlString("Прочитано");

            return new MvcHtmlString("Не прочитано");
        }
    }
}