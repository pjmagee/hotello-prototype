using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Hotello.UI.Web.Helpers
{
    public static class PropertyInfoExtensions
    {
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static bool AttributeExists<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static T GetAttribute<T>(this Type type) where T : class
        {
            return type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }
		
        public static string LabelFromType(Type type)
        {
            var att = GetAttribute<DisplayNameAttribute>(type);

            return att != null ? att.DisplayName : type.Name.ToSeparatedWords();
        }
		
        public static string GetLabel(this Object Model)
        {
            return LabelFromType(Model.GetType());
        }

        public static string GetLabel(this IEnumerable Model)
        {
            var elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return LabelFromType(elementType);
        }
    }
}