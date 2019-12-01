using System.Web;
using System.Web.Mvc;

namespace MVCFramework.Models

{
    public enum SessionBaseName
    {
        abc,
        UserName,
        UserId,
        MaxFileId,
        FieldId
    }
    public static class HttpSessionStateManager
    {

        public static void SetVaue(SessionBaseName baseName, object value)
        {
            HttpContext.Current.Session[baseName.ToString()] = value.ToString();
        }
        public static string GetValue(SessionBaseName baseName)
        {
            var session = HttpContext.Current.Session[baseName.ToString()];

            if (session == null)
            {
                return "null";
            }

            return session.ToString();
        }


    }
}