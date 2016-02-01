using System.Collections.Generic;
using System.Reflection;
using Core.Common.Core;

namespace Core.Common.Extensions
{
    public static class CoreExtensions
    {
        private static readonly Dictionary<string, bool> BrowsableProperties = new Dictionary<string, bool>();
        private static readonly Dictionary<string, PropertyInfo[]> BrowsablePropertyInfos = new Dictionary<string, PropertyInfo[]>();

        public static bool IsNavigable(this PropertyInfo property)
        {
            var navigable = true;

            var attributes = property.GetCustomAttributes(typeof (NotNavigableAttribute), true);

            if (attributes.Length > 0)
                navigable = false;

            return navigable;
        }




        public static PropertyInfo[] GetBrowsableProperties(this object obj)
        {
            var key = obj.GetType().ToString();

            if (!BrowsablePropertyInfos.ContainsKey(key))
            {
                var propertyInfoList = new List<PropertyInfo>();
                var properties = obj.GetType().GetProperties();

                foreach (var property in properties)
                {
                    if ((property.PropertyType.IsSubclassOf(typeof(ObjectBase)) || property.PropertyType.GetInterface("IList") != null))
                    {
                        // only add to list of the property is NOT marked with [NotNavigable]
                        if(IsBrowsable(obj, property))
                            propertyInfoList.Add(property);
                    }
                }

                BrowsablePropertyInfos.Add(key, propertyInfoList.ToArray());

            }

            return BrowsablePropertyInfos[key];
        }

        private static bool IsBrowsable(this object obj, PropertyInfo property)
        {
            var key = $"{obj.GetType()} .{property.Name}";

            if (!BrowsableProperties.ContainsKey(key))
            {
                var browsable = property.IsNavigable();
                BrowsableProperties.Add(key, browsable);
            }

            return BrowsableProperties[key];

        }
    }
}