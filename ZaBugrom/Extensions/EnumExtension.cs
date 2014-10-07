using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;
using Models.Data.Enums;

namespace ZaBugrom.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }

        public static MvcHtmlString GetPostClassByType(this HtmlHelper helper, PostType type)
        {
            switch (type)
            {
                case PostType.SimplePost:
                    return new MvcHtmlString("simple-post");
                case PostType.VideoPost:
                    return new MvcHtmlString("video-post");
                default:
                    throw new InvalidEnumArgumentException("Unknown post type!");
            }
        }

        public static MvcHtmlString GetMesageMarkClassByType(this HtmlHelper helper, MessageType type)
        {
            switch (type)
            {
                case MessageType.NewContent:
                    return new MvcHtmlString("new-content");
                case MessageType.Notification:
                    return new MvcHtmlString("notification");
                case MessageType.UserMail:
                    return new MvcHtmlString("user-mail");
                default:
                    throw new InvalidEnumArgumentException("Unknown message type!");
            }
        }
    }
}