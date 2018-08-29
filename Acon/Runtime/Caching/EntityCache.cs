// ***********************************************************************
// Assembly         : Acon.Runtime.Caching
// Author           : junwei.wu
// Created          : 2018/1/9 16:19:23
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 16:19:23:
// ***********************************************************************
// <copyright file="EntityCache" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Acon.Runtime.Caching.Memory;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Runtime.Caching
{
    public abstract class EntityCache<T> where T : new()
    {
        protected ICacheManager CacheManager { get; private set; }

        protected string CacheName { get; private set; }

        public ITypedCache<string, T> InternalCache
        {
            get
            {
                return CacheManager.GetCache<string, T>(CacheName);
            }
        }

        public EntityCache(ICacheManager cacheManager, string cacheName)
        {
            this.CacheManager = cacheManager;
            this.CacheName = !string.IsNullOrEmpty(cacheName) ? cacheName : GenerateDefaultCacheName();
        }

        //public EntityCache(string cacheName,ILog log)
        //    : this(new MemoryCacheManager(log), cacheName)
        //{

        //}

        //public EntityCache(ILog log)
        //    : this(string.Empty,log)
        //{

        //}

        private string GenerateDefaultCacheName()
        {
            return this.GetType().FullName;
        }

        public void Clear()
        {
            InternalCache.Clear();
        }
    }
}
