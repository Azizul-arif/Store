using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Ecommerce.SessionHelper
{
    public static class SessionHelper
    {
        public static void SetObjInSession(this ISession session,string key,object value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            session.SetString(key, JsonConvert.SerializeObject(value, settings));
        }

        public static List<T> GetObjInSession<T>(this ISession session,string key)
        {
            var obj=session.GetString(key);
            return obj == null ? default(List<T>) : JsonConvert.DeserializeObject<List<T>>(obj);
        }

        public static T GetSessionObj<T>(this ISession session ,string key)
        {
            var obj=session.GetString(key);
            return obj == null ? default(T) : JsonConvert.DeserializeObject<T>(obj);

        }


    }
}
