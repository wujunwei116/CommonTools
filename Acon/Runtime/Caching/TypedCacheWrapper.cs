// ***********************************************************************
// Assembly         : Acon.Runtime.Caching
// Author           : junwei.wu
// Created          : 2018/1/9 16:06:58
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 16:06:58:
// ***********************************************************************
// <copyright file="TypedCacheWrapper" company="ACON">
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
    public class TypedCacheWrapper<TKey, TValue> : ITypedCache<TKey, TValue>
    {

        #region ITypedCache<TKey,TValue> 成员

        public string Name
        {
            get { return InternalCache.Name; }
        }

        public TimeSpan DefaultSlidingExpireTime
        {
            get { return InternalCache.DefaultSlidingExpireTime; }
            set { InternalCache.DefaultSlidingExpireTime = value; }
        }

        public ICache InternalCache { get; private set; }
        /// <param name="internalCache">The actual internal cache</param>
        public TypedCacheWrapper(ICache internalCache)
        {
            InternalCache = internalCache;
        }

        public TValue Get(TKey key, Func<TKey, TValue> factory)
        {
            return InternalCache.Get(key, factory);
        }



        public TValue GetOrDefault(TKey key)
        {
            return InternalCache.GetOrDefault<TKey, TValue>(key);
        }


        public void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            InternalCache.Set(key.ToString(), value, slidingExpireTime);
        }



        public void Remove(TKey key)
        {
            InternalCache.Remove(key.ToString());
        }



        public void Clear()
        {
            InternalCache.Clear();
        }



        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            InternalCache.Dispose();
        }

        #endregion
    }
}
