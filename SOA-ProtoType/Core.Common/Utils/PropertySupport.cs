using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Common.Utils
{
    public static class PropertySupport
    {
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if(propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            var memberExpression = propertyExpression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException(nameof(memberExpression));

            var property = memberExpression.Member as PropertyInfo;

            if(property == null)
                throw new ArgumentException(nameof(property));

            var getMethod = property.GetGetMethod(true);

            if(getMethod.IsStatic)
                throw new ArgumentException(nameof(getMethod));

            return memberExpression.Member.Name;

        }
         
    }
}