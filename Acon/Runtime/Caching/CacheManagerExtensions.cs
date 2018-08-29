// ***********************************************************************
// Assembly         : Acon.Runtime.Caching
// Author           : junwei.wu
// Created          : 2018/1/9 16:08:18
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 16:08:18:
// ***********************************************************************
// <copyright file="CacheManagerExtensions" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Runtime.Caching
{
    public static class CacheManagerExtensions
    {
        public static ITypedCache<TKey, TValue> GetCache<TKey, TValue>(this ICacheManager cacheManager, string name)
        {
            return cacheManager.GetCache(name).AsTyped<TKey, TValue>();
        }
    }
}
