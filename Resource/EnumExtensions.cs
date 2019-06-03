using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Resource
{
    public static class EnumExtension
    {
        public static string ToLocalizedString(this Enum en)
        {
            Type type = en.GetType();
            var attribute = type.GetTypeInfo().GetCustomAttribute<EnumResourceAttribute>();
            if(attribute != null)
            {
                var rsManager = new ResourceManager(attribute.ResourceType);
                return rsManager.GetString(en.ToString());
            }
            return en.ToString();
        }
    }
}
