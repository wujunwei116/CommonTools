// ***********************************************************************
// Assembly         : Acon.Dapper.Filters.Action
// Author           : junwei.wu
// Created          : 2018/1/9 15:48:26
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:48:26:
// ***********************************************************************
// <copyright file="DapperActionFilterExecuter" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Dapper.Filters.Action
{
   public class DapperActionFilterExecuter : IDapperActionFilterExecuter
    {
        private IDapperActionFilter createionAuditFiter;
        private IDapperActionFilter modifycationFiter;
        private IDapperActionFilter deleteionAuditFiter;

        public DapperActionFilterExecuter()
        {
            createionAuditFiter = new CreationAuditDapperActionFilter();
            modifycationFiter = new ModificationAuditDapperActionFilter();
            deleteionAuditFiter = new DeletionAuditDapperActionFilter();
        }

        #region IDapperActionFilterExecuter 成员

        public void ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class
        {
            createionAuditFiter.ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }

        public void ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class
        {
            modifycationFiter.ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }

        public void ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class
        {
            deleteionAuditFiter.ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }

        #endregion
    }
}
