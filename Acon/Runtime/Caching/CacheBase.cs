// ***********************************************************************
// Assembly         : Acon.Runtime.Caching
// Author           : junwei.wu
// Created          : 2018/1/9 16:09:00
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 16:09:00:
// ***********************************************************************
// <copyright file="CacheBase" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Runtime.Caching
{
    public abstract class CacheBase : ICache
    {
        public ILog Logger { get; set; }

        public string Name { get; private set; }

        public TimeSpan DefaultSlidingExpireTime { get; set; }

        public TimeSpan? DefaultAbsoluteExpireTime { get; set; }


        protected readonly object SyncObj = new object();

        //private readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        protected CacheBase(string name,ILog log)
        {
            Name = name;
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);
            Logger = log;
        }

        

        public virtual object Get(string key, Func<string, object> factory)
        {
            object item = null;

            try
            {
                item = GetOrDefault(key);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), ex);
            }

            if (item == null)
            {
                lock (SyncObj)
                {
                    try
                    {
                        item = GetOrDefault(key);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.ToString(), ex);
                    }

                    if (item == null)
                    {
                        item = factory(key);

                        if (item == null)
                        {
                            return null;
                        }

                        try
                        {
                            Set(key, item);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex.ToString(), ex);
                        }
                    }
                }
            }

            return item;
        }

        public abstract object GetOrDefault(string key);

        public abstract void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        public abstract void Remove(string key);

        public abstract void Clear();

        public virtual void Dispose()
        {

        }
    }
}
