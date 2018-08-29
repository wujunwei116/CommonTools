using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Acon.Dapper.Filters.Query
{
    public interface IDapperQueryFilter
    {
        string FilterName { get; }

        bool IsEnabled { get; }

        IFieldPredicate ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class;

        Expression<Func<TEntity, bool>> ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    }
}
