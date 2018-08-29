using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Reflection
{
    public interface IFastReflectionPool<TKeyType, TAccessor>
    {
        TAccessor Get(Type type, TKeyType key);
    }
}
