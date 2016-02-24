using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace XplicitApp.Infrastracture.Utils
{
    public static class JsonExtensions
    {
         // Converts any object into json.
        public static string ToJson<T>(this T obj, bool includeNull = true)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[] {new StringEnumConverter()},
                NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}