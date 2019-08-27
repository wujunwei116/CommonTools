// ***********************************************************************
// Assembly         : Acon.Dapper.Data
// Author           : junwei.wu
// Created          : 2018/1/9 15:36:26
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:36:26:
// ***********************************************************************
// <copyright file="DBSession" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Acon.Dapper.Data
{
    public class DBSession : IDisposable
    {
        public DbConnection Connection { get; private set; }

        public DbTransaction CurrentTransaction { get; private set; }

        public string ProviderName { get; set; }

        public string ConnectionString { get; private set; }
        public DBSession(string providerName, DbConnection connection)
        {
            // TODO: Complete member initialization
            this.ProviderName = providerName;
            this.ConnectionString = connection.ConnectionString;
            this.Connection = connection;
        }

        public DbTransaction BeginTransation()
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            CurrentTransaction = Connection.BeginTransaction();
            return CurrentTransaction;
        }

        public void Commit()
        {
            CurrentTransaction.Commit();
            CurrentTransaction = null;
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            if (CurrentTransaction == null) return;
            CurrentTransaction.Rollback();
            CurrentTransaction.Dispose();
            CurrentTransaction = null;
        }

        public static DBSession From(string providerName, string connectionString)
        {
            DbConnection connection = CreateConnection(providerName, connectionString);
            return new DBSession(providerName, connection);
        }

        private static DbConnection CreateConnection(string providerName, string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(providerName);
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public static DBSession FromApplicationSettings(string key)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[key];
            return From(connectionStringSettings.ProviderName, connectionStringSettings.ConnectionString);
        }


        #region IDisposable 成员

        private bool _isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (Connection != null)
                {
                    using (Connection)
                    {
                        if (Connection.State != ConnectionState.Closed)
                        {
                            if (CurrentTransaction != null)
                            {
                                CurrentTransaction.Rollback();
                                CurrentTransaction.Dispose();
                            }
                        }
                        Connection.Close();
                        Connection.Dispose();
                    }
                }
                this._isDisposed = true;
            }
        }
        #endregion
    }
}
