using System;
using System.Web.Mvc;

namespace Test
{
    public class AsyncLoader

    {

        public static MvcHtmlString Render(string Controler, string Action, string PlaceHolder)
        {
            return Render(Controler, Action, PlaceHolder, null);
        }

        public static MvcHtmlString Render(string Controler, string Action, string PlaceHolder, dynamic Model)
        {
            var model = ObjectToJason(Model);
            string html = $" <Script>\r window.async.getFromController('/{Controler}/{Action}', '{PlaceHolder}', {model}); ; \r</Script>";
            return MvcHtmlString.Create(html);
        }

        private static object ObjectToJason(dynamic obj)
        {
            var json = "null";
            if (obj == null)
                return json;
            try
            {
                var serializerSettings = CreateSerializerSettings();
                json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                //        Newtonsoft.Json.Formatting.None,
                //        serializerSettings);
            }
            catch
            {
                //log this
            }
            return json;
        }

        private static object CreateSerializerSettings()
        {
            var serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            serializerSettings.MaxDepth = 5;
            serializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            serializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            serializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializerSettings.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Reuse;
            serializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
            return serializerSettings;
        }
        public static MvcHtmlString Action(string Controler, string Action, string PlaceHolder, string Link)
        {
            return AsyncLoader.Action(Controler, Action, PlaceHolder, Link, null);
        }
        public static MvcHtmlString Action(string Controler, string Action, string PlaceHolder, string Link, dynamic Model)
        {
            var model = ObjectToJason(Model);
            string html = $"$('#{Link}').click(function(){{ window.async.getFromController('/{Controler}/{Action}', '{PlaceHolder}', {model}); }}); ";
            return MvcHtmlString.Create(html);
        }
        public static MvcHtmlString Post(string Controler, string Action, string PlaceHolder)
        {
            string html = $@" window.async.postToController('/{Controler}/{Action}', '{PlaceHolder}'); ";
            return MvcHtmlString.Create(html);
        }

    }
}
