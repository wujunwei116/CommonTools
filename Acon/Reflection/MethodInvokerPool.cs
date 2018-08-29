﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Acon.Reflection
{
    public class MethodInvokerPool : FastReflectionPool<string, IMethodInvoker>
    {
        protected override IMethodInvoker Create(Type type, string key)
        {
            if (type == null || String.IsNullOrEmpty(key))
            {
                Debug.Assert(false, "type 或 key 为空");
                throw new ArgumentNullException();
            }

            MethodInfo methodInfo = type.GetMethod(key);

            if (methodInfo == null)
            {
                Debug.Assert(false, "没有指定的MethodInfo:" + key);
                throw new MissingMemberException(key);
            }

            return new MethodInvoker(methodInfo);
        }
    }
}
