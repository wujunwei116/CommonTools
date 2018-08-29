// ***********************************************************************
// Assembly         : AconEntityFramework.DapperExtensions.Interception
// Author           : junwei.wu
// Created          : 2018/1/5 16:45:31
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/5 16:45:31:
// ***********************************************************************
// <copyright file="DbInterception" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DapperExtensions
{
   public static  class DbInterception
    {
       private static List<IDbInterceptor> interceptors = new List<IDbInterceptor>();

       public static List<IDbInterceptor> Interceptors
       {
           get { return interceptors; }
       }

       public static void Add(IDbInterceptor interceptor)
       {
           if (!interceptors.Contains(interceptor))
           {
               interceptors.Add(interceptor);
           }
       }

       public static void Remove(IDbInterceptor interceptor)
       {
           interceptors.Remove(interceptor);
       }
    }
}
