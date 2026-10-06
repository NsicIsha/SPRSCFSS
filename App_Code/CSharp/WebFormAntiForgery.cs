using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// Summary description for WebFormAntiForgery
/// </summary>
 
    /// <summary>Utility to provide MVC anti forgery tokens in WebForms pages</summary>
    public class WebFormAntiForgery
    {
        /// <summary>Create an anti forgery token in a WebForms page</summary>
        /// <returns>The HTML input and sets the cookie</returns>
        public static string AntiForgeryToken()
        {
            string formValue = GetAntiForgeryTokenAndSetCookie();

            // string fieldName = AntiForgeryData.GetAntiForgeryTokenName(null);
            var mvcAssembly = typeof(HtmlHelper).Assembly;
            var afdType = mvcAssembly.GetType("System.Web.Mvc.AntiForgeryData");
            string fieldName = Convert.ToString(afdType.InvokeMember(
                "GetAntiForgeryTokenName",
                BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod,
                null,
                null,
                new object[] { null }));

            TagBuilder builder = new TagBuilder("input");
            builder.Attributes["type"] = "hidden";
            builder.Attributes["name"] = fieldName;
            builder.Attributes["value"] = formValue;
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        static string GetAntiForgeryTokenAndSetCookie()
        {
            var mvcAssembly = typeof(HtmlHelper).Assembly;
            var afdType = mvcAssembly.GetType("System.Web.Mvc.AntiForgeryData");

            // new AntiForgeryDataSerializer();
            var serializerType = mvcAssembly.GetType("System.Web.Mvc.AntiForgeryDataSerializer");
            var serializerCtor = serializerType.GetConstructor(new Type[0]);
            object serializer = serializerCtor.Invoke(new object[0]);

            // string cookieName = AntiForgeryData.GetAntiForgeryTokenName(HttpContext.Current.Request.ApplicationPath);
            string cookieName = Convert.ToString(afdType.InvokeMember(
                "GetAntiForgeryTokenName",
                BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod,
                null,
                null,
                new object[] { HttpContext.Current.Request.ApplicationPath }));

            // AntiForgeryData cookieToken;
            object cookieToken;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                // cookieToken = Serializer.Deserialize(cookie.Value);
                cookieToken = serializerType.InvokeMember("Deserialize", BindingFlags.InvokeMethod, null, serializer, new object[] { cookie.Value });
            }
            else
            {
                // cookieToken = AntiForgeryData.NewToken();
                cookieToken = afdType.InvokeMember(
                    "NewToken",
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod,
                    null,
                    null,
                    new object[0]);

                // string cookieValue = Serializer.Serialize(cookieToken);
                string cookieValue = Convert.ToString(serializerType.InvokeMember("Serialize", BindingFlags.InvokeMethod, null, serializer, new object[] { cookieToken }));

                var newCookie = new HttpCookie(cookieName, cookieValue) { HttpOnly = true };

                HttpContext.Current.Response.Cookies.Set(newCookie);
            }

            // AntiForgeryData formToken = new AntiForgeryData(cookieToken)
            // {
            //     CreationDate = DateTime.Now,
            //     Salt = salt
            // };
            var ctor = afdType.GetConstructor(new Type[] { afdType });
            object formToken = ctor.Invoke(new object[] { cookieToken });

            afdType.InvokeMember("CreationDate", BindingFlags.SetProperty, null, formToken, new object[] { DateTime.Now });
            afdType.InvokeMember("Salt", BindingFlags.SetProperty, null, formToken, new object[] { null });

            // string formValue = Serializer.Serialize(formToken);
            string formValue = Convert.ToString(serializerType.InvokeMember("Serialize", BindingFlags.InvokeMethod, null, serializer, new object[] { formToken }));
            return formValue;
        }
    }
 