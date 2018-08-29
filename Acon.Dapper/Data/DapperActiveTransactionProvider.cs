// ***********************************************************************
// Assembly         : Acon.Dapper.Data
// Author           : junwei.wu
// Created          : 2018/1/9 15:38:04
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:38:04:
// ***********************************************************************
// <copyright file="DapperActiveTransactionProvider" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Acon.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Acon.Dapper.Data
{
    public class DapperActiveTransactionProvider : IActiveTransactionProvider
    {
          private DBSession Session { get;  set; }

          public DapperActiveTransactionProvider(DBSession session)
        {
            Session = session;
        }

        #region IActiveTransactionProvider 成员

        public IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args)
        {
            return Session.CurrentTransaction;
        }


        public IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args)
        {
            return Session.Connection;
        }

        #endregion

    }
}
