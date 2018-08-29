// ***********************************************************************
// Assembly         : Acon.Reflection
// Author           : junwei.wu
// Created          : 2018/8/29 9:17:04
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/8/29 9:17:04:
// ***********************************************************************
// <copyright file="FieldAccessorPool" company="ACON">
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
    public class FieldAccessorPool : FastReflectionPool<string, IFieldAccessor>
    {
        protected override IFieldAccessor Create(Type type, string key)
        {
            if (type == null || String.IsNullOrEmpty(key))
            {
                Debug.Assert(false, "type 或 key 为空");
                throw new ArgumentNullException();
            }

            FieldInfo fieldInfo = type.GetField(key);

            if (fieldInfo == null)
            {
                Debug.Assert(false, "没有指定的FieldInfo:" + key);
                throw new MissingFieldException(key);
            }

            return new FieldAccessor(fieldInfo);
        }
    }
}
