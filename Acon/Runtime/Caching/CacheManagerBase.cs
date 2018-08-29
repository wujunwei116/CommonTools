// ***********************************************************************
// Assembly         : Acon.Runtime.Caching
// Author           : junwei.wu
// Created          : 2018/1/9 16:09:11
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 16:09:11:
// ***********************************************************************
// <copyright file="CacheManagerBase" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Runtime.Caching
{
    public abstract class CacheManageBase : ICacheManager
    {
        protected readonly ConcurrentDictionary<string, ICache> Caches;
        public ILog Logger { get; set; }

        protected CacheManageBase(ILog log)
        {
            Caches = new ConcurrentDictionary<string, ICache>();
            this.Logger = log;
        }


        #region ICacheManager 成员

        public IList<ICache> GetAllCaches()
        {
            return Caches.Values.ToList();
        }

        public ICache GetCache(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name 不能为空！");
            return Caches.GetOrAdd(name, cacheName =>
            {
                var cache = CreateCacheImplementation(cacheName);
                return cache;
            });
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Caches.Clear();
        }

        #endregion

        /// <summary>
        /// Used to create actual cache implementation.
        /// </summary>
        /// <param name="name">Name of the cache</param>
        /// <returns>Cache object</returns>
        protected abstract ICache CreateCacheImplementation(string name);
    }
}
