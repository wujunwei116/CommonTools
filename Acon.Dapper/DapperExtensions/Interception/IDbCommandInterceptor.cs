// ***********************************************************************
// Assembly         : AconEntityFramework.DapperExtensions.Interception
// Author           : junwei.wu
// Created          : 2018/1/5 16:56:51
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/5 16:56:51:
// ***********************************************************************
// <copyright file="IDbCommandInterceptor" company="ACON">
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
   public interface IDbCommandInterceptor :IDbInterceptor
    {
        void ExecuteCommand(string sql);
    }
}
