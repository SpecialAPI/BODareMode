using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode.Serializer
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RDSForceSerializeAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class RDSDontSerializeAttribute : Attribute
    {
    }
}
