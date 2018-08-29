// ***********************************************************************
// Assembly         : Acon.Dapper.Filters.Query
// Author           : junwei.wu
// Created          : 2018/1/9 15:45:09
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:45:09:
// ***********************************************************************
// <copyright file="DapperQueryFilterExecuter" company="ACON">
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
using Acon.Dapper.Expressions;

namespace Acon.Dapper.Filters.Query
{
   public class DapperQueryFilterExecuter : IDapperQueryFilterExecuter
    {
        private readonly IEnumerable<IDapperQueryFilter> _queryFilters;

        public DapperQueryFilterExecuter()
        {
            _queryFilters = new List<IDapperQueryFilter>();
        }

        #region IDapperQueryFilterExecuter 成员

        public IPredicate ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            ICollection<IDapperQueryFilter> filters = _queryFilters.ToList();

            foreach (IDapperQueryFilter filter in filters)
            {
                predicate = filter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            }

            IPredicate pg = predicate.ToPredicateGroup<TEntity, TPrimaryKey>();
            return pg;
        }

        public PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class
        {
            ICollection<IDapperQueryFilter> filters = _queryFilters.ToList();
            var groups = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            foreach (IDapperQueryFilter filter in filters)
            {
                IFieldPredicate predicate = filter.ExecuteFilter<TEntity, TPrimaryKey>();
                if (predicate != null)
                {
                    groups.Predicates.Add(predicate);
                }
            }

            return groups;
        }

        #endregion
    }
}
