namespace Microsoft.Web.Mvc {
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    public static class AjaxExtensions
    {
        /// <summary>
        /// Use to sanitize inputs of any nasty Javascript scripts/tags. Use alongside and in addition to Html.Encode()! http://live.visitmix.com/MIX10/Sessions/FT05
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string JavaScriptStringEncode(this AjaxHelper helper, string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                return message;
            }

            StringBuilder builder = new StringBuilder();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.Serialize(message, builder);
            return builder.ToString(1, builder.Length - 2); // remove first + last quote
        }
        public static string JavaScriptStringEncode(this AjaxHelper helper, object message)
        {
            return JavaScriptStringEncode(helper, message as string);
        }
        /// <summary>
        /// Use to sanitize inputs of any nasty Javascript scripts/tags. Use alongside and in addition to Html.Encode()! http://live.visitmix.com/MIX10/Sessions/FT05
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string JavaScriptStringEncode(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                return message;
            }

            StringBuilder builder = new StringBuilder();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.Serialize(message, builder);
            return builder.ToString(1, builder.Length - 2); // remove first + last quote

        }

    }
}