// ***********************************************************************
// Assembly         : Acon.Dapper.Expressions
// Author           : junwei.wu
// Created          : 2018/1/9 15:54:48
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:54:48:
// ***********************************************************************
// <copyright file="DapperExpressionExtensions" company="ACON">
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

namespace Acon.Dapper.Expressions
{
    internal static class DapperExpressionExtensions
    {
        public static IPredicate ToPredicateGroup<TEntity, TPrimaryKey>(this Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            Check.NotNull(expression, "Expression");

            var dev = new DapperExpressionVisitor<TEntity, TPrimaryKey>();
            IPredicate pg = dev.Process(expression);
            return pg;
        }
    }
}
