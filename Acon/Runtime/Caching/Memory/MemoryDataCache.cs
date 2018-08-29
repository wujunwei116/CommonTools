// ***********************************************************************
// Assembly         : Acon.Runtime.Caching.Memory
// Author           : junwei.wu
// Created          : 2018/1/9 16:13:35
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 16:13:35:
// ***********************************************************************
// <copyright file="MemoryDataCache" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace Acon.Runtime.Caching.Memory
{
    /// <summary>
    /// 内存中的缓存
    /// </summary>
    public class MemoryDataCache : CacheBase
    {
        private MemoryCache _memoryCache;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Unique name of the cache</param>
        public MemoryDataCache(string name,ILog log)
            : base(name,log)
        {
            _memoryCache = new MemoryCache(Name);
        }


        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new Exception("Can not insert null values to the cache!");
            }

            var cachePolicy = new CacheItemPolicy();

            if (absoluteExpireTime != null)
            {
                cachePolicy.AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteExpireTime.Value);
            }
            else if (slidingExpireTime != null)
            {
                cachePolicy.SlidingExpiration = slidingExpireTime.Value;
            }
            else if (DefaultAbsoluteExpireTime != null)
            {
                cachePolicy.AbsoluteExpiration = DateTimeOffset.Now.Add(DefaultAbsoluteExpireTime.Value);
            }
            else
            {
                cachePolicy.SlidingExpiration = DefaultSlidingExpireTime;
            }

            _memoryCache.Set(key, value, cachePolicy);
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public override void Clear()
        {
            _memoryCache.Dispose();
            _memoryCache = new MemoryCache(Name);
        }

        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }
    }
}
