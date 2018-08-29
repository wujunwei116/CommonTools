// ***********************************************************************
// Assembly         : Acon.Dapper.Filters.Query
// Author           : junwei.wu
// Created          : 2018/1/9 15:43:52
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:43:52:
// ***********************************************************************
// <copyright file="IDapperQueryFilterExecuter" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Acon.Dapper.Filters.Query
{
    public interface IDapperQueryFilterExecuter
    {
        IPredicate ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class;
    }
}
