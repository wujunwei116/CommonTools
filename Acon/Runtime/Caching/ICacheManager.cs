// ***********************************************************************
// Assembly         : Acon.Runtime.Caching
// Author           : junwei.wu
// Created          : 2018/1/9 16:05:31
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 16:05:31:
// ***********************************************************************
// <copyright file="ICacheManager" company="ACON">
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
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// Gets all caches.
        /// </summary>
        /// <returns>List of caches</returns>
        IList<ICache> GetAllCaches();

        /// <summary>
        /// Gets a <see cref="ICache"/> instance.
        /// It may create the cache if it does not already exists.
        /// </summary>
        /// <param name="name">
        /// Unique and case sensitive name of the cache.
        /// </param>
        /// <returns>The cache reference</returns>
        ICache GetCache(string name);
    }
}
