// ***********************************************************************
// Assembly         : Acon.Dapper.Filters.Action
// Author           : junwei.wu
// Created          : 2018/1/9 15:44:07
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:44:07:
// ***********************************************************************
// <copyright file="IDapperActionFilterExecuter" company="ACON">
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
    public interface IDapperActionFilterExecuter
    {
        void ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class;

        void ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class;

        void ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class;
    }
}
