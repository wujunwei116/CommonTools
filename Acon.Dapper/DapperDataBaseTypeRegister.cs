// ***********************************************************************
// Assembly         : Acon.Dapper
// Author           : junwei.wu
// Created          : 2018/1/9 15:57:46
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:57:46:
// ***********************************************************************
// <copyright file="DapperDataBaseTypeRegister" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Acon.Dapper.Data;
using DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Dapper
{
    public static class DapperDataBaseTypeRegister
    {
       private static Dictionary<string, Type> dbTypes = new Dictionary<string, Type>
       {
            {"System.Data.SqlClient",typeof(SqlServerDialect)},
            {"MySql.Data.MySqlClient",typeof(MySqlDialect)},
            {"System.Data.Odbc",typeof(DB2Dialect)},
            {"System.Data.OracleClient",typeof(OracleDialect)},
            //{"MySql.Data.MySqlClient",typeof(PostgreSqlDialect)},
            {"System.Data.SqlServerCe",typeof(SqlCeDialect)},
            {"System.Data.SQLite",typeof(SqliteDialect)}
       };

        public static void RegisterActiveDb(DBSession session)
        {
            RegisterActiveDb(session.ProviderName);
        }

        public static void RegisterActiveDb(string providerName)
        {
            Type databaseType;
            if (dbTypes.TryGetValue(providerName, out databaseType))
            {
                var dialect = (ISqlDialect)Activator.CreateInstance(databaseType);
                DapperExtensions.DapperExtensions.SqlDialect = dialect;
            }
            else
                throw new Exception(string.Format("数据库:{0} 类型不支持", providerName));
        }

        public static void RegisterActiveDb(string providerName, ISqlDialect sqlDialect)
        {
            RegisterActiveDb(providerName, sqlDialect.GetType());
        }

        public static void RegisterActiveDb(string providerName, Type sqlDialect)
        {
            AddOrReplaceSqlDialect(providerName, sqlDialect);
            RegisterActiveDb(providerName);
        }

        private static void AddOrReplaceSqlDialect(string providerName, Type sqlDialect)
        {
            Type databaseType;
            if (dbTypes.TryGetValue(providerName, out databaseType))
            {
                dbTypes[providerName] = sqlDialect;
            }
            else
            {
                dbTypes.Add(providerName, sqlDialect);
            }
        }
    }
}
