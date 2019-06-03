using Resource.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resource
{
    [EnumResource(typeof(GenderEnumResource))]
    public enum GenderEnum
    {
        Male,
        Female,
        Unspecified
    }
}
