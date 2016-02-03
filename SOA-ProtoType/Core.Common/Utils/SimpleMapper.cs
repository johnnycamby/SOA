using System;
using System.Linq;
using System.Reflection;

namespace Core.Common.Utils
{
    public static class SimpleMapper
    {
        public static void PropertyMap<T, TU>(T source, TU destination) 
            where T : class , new()
            where TU : class , new()
        {

            var sourceProperties = source.GetType().GetProperties().ToList<PropertyInfo>();
            var destinationPropeties = destination.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationPropeties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (ArgumentException)
                    {
                        
                    }
                }
            }

        }
    }
}