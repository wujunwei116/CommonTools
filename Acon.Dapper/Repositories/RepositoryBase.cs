// ***********************************************************************
// Assembly         : Acon.Dapper.Repositories
// Author           : junwei.wu
// Created          : 2018/1/9 15:42:23
//
// Last Modified By : junwei.wu
// Last Modified On : 2018/1/9 15:42:23:
// ***********************************************************************
// <copyright file="RepositoryBase" company="ACON">
//     Copyright (c) ACON. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Acon.Dapper.Filters.Action;
using Acon.Dapper.Filters.Query;
using Acon.Data;
using Acon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dapper;
using DapperExtensions;
using Acon.Dapper.Extensions;

namespace Acon.Dapper.Repositories
{
    public class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        private readonly IActiveTransactionProvider _activeTransactionProvider;

        public virtual DbConnection Connection
        {
            get { return (DbConnection)_activeTransactionProvider.GetActiveConnection(ActiveTransactionProviderArgs.Empty); }
        }
        public virtual DbTransaction ActiveTransaction
        {
            get { return (DbTransaction)_activeTransactionProvider.GetActiveTransaction(ActiveTransactionProviderArgs.Empty); }
        }

        public IDapperQueryFilterExecuter DapperQueryFilterExecuter { get; set; }
        public IDapperActionFilterExecuter DapperActionFilterExecuter { get; set; }

        public RepositoryBase(IActiveTransactionProvider provider)
        {
            _activeTransactionProvider = provider;
            DapperQueryFilterExecuter = new DapperQueryFilterExecuter();
            DapperActionFilterExecuter = new DapperActionFilterExecuter();
        }

        #region IRepository<TEntity,TPrimaryKey> 成员

        public TEntity Single(TPrimaryKey id)
        {
            return Single(CreateEqualityExpressionForId(id));
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate pg = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg, transaction: ActiveTransaction).Single();
        }

        public TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            return item;
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate pg = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg, transaction: ActiveTransaction).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            PredicateGroup predicateGroup = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>();
            return Connection.GetList<TEntity>(predicateGroup, transaction: ActiveTransaction);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate pg = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetList<TEntity>(pg, transaction: ActiveTransaction);
        }

        public IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);

            return Connection.GetPage<TEntity>(
                filteredPredicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                pageNumber,
                itemsPerPage,
                ActiveTransaction);
        }

        public IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetPage<TEntity>(filteredPredicate, sortingExpression.ToSortable(ascending), pageNumber, itemsPerPage, ActiveTransaction);
        }

        public IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            return Connection.Query<TEntity>(query, parameters, ActiveTransaction);
        }

        public IEnumerable<TAny> Query<TAny>(string query, object parameters = null) where TAny : class
        {
            return Connection.Query<TAny>(query, parameters, ActiveTransaction);
        }

        public IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetSet<TEntity>(
                filteredPredicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                firstResult,
                maxResults,
                ActiveTransaction
            );
        }

        public IEnumerable<TEntity> GetSet(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.GetSet<TEntity>(filteredPredicate, sortingExpression.ToSortable(ascending), firstResult, maxResults, ActiveTransaction);
        }

        public void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            DapperActionFilterExecuter.ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(entity);
            TPrimaryKey primaryKey = Connection.Insert(entity, ActiveTransaction);
            return primaryKey;
        }

        public void Update(TEntity entity)
        {
            DapperActionFilterExecuter.ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(entity);
            Connection.Update(entity, ActiveTransaction);
        }


        public void Delete(TEntity entity)
        {
            if (entity is ISoftDelete)
            {
                DapperActionFilterExecuter.ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(entity);
                Connection.Update(entity, ActiveTransaction);
            }
            else
            {
                Connection.Delete(entity, ActiveTransaction);
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> items = GetAll(predicate);
            foreach (TEntity entity in items)
            {
                Delete(entity);
            }
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate filteredPredicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return Connection.Count<TEntity>(filteredPredicate, ActiveTransaction);
        }

        public int Execute(string query, object parameters = null)
        {
            return Connection.Execute(query, parameters, ActiveTransaction);
        }

        #endregion

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            ParameterExpression lambdaParam = Expression.Parameter(typeof(TEntity));

            BinaryExpression lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }

    public class RepositoryBase<TEntity> : RepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class
    {
        public RepositoryBase(IActiveTransactionProvider activeTransactionProvider)
            : base(activeTransactionProvider)
        {
        }
    }
}
