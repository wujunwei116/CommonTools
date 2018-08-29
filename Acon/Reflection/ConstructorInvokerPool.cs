// ***********************************************************************
// Assembly         : Acon.Reflection
// Author           : junwei.wu
// Created          : 2018/8/29 9:15:04
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/8/29 9:15:04:
// ***********************************************************************
// <copyright file="ConstructorInvokerPool" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Acon.Reflection
{
    public class ConstructorInvokerPool : FastReflectionPool<Type[], IConstructorInvoker>
    {
        public ConstructorInvokerPool()
        {
            CustomCompare = true;
        }

        protected override bool Compare(Type[] key1, Type[] key2)
        {
            return key1.SequenceEqual(key2);
        }

        protected override IConstructorInvoker Create(Type type, Type[] key)
        {
            if (type == null || key == null)
            {
                Debug.Assert(false, "type 或 key 为空");
                throw new ArgumentNullException();
            }

            ConstructorInfo constructorInfo = type.GetConstructor(key);

            if (constructorInfo == null)
            {
                Debug.Assert(false, "没有指定的ConstructorInfo");
                throw new InvalidOperationException();
            }

            return new ConstructorInvoker(constructorInfo);
        }
    }
}
