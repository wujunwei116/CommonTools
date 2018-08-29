// ***********************************************************************
// Assembly         : Acon.Runtime.Caching.Memory
// Author           : junwei.wu
// Created          : 2018/1/9 16:15:12
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 16:15:12:
// ***********************************************************************
// <copyright file="MemoryCacheManager" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Runtime.Caching.Memory
{
    public class MemoryCacheManager : CacheManageBase
    {

        public MemoryCacheManager(ILog log)
            : base(log)
        {

        }
        protected override ICache CreateCacheImplementation(string name)
        {
            return new MemoryDataCache(name, Logger);
        }
    }
}
