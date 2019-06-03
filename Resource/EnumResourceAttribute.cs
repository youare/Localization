using System;
using System.Collections.Generic;
using System.Text;

namespace Resource
{
    public class EnumResourceAttribute:Attribute
    {
        public Type ResourceType { get; }

        public EnumResourceAttribute(Type resourceType)
        {
            ResourceType = resourceType;
        }
    }
}
