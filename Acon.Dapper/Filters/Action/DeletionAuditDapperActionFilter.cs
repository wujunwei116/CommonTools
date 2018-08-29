using Acon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Dapper.Filters.Action
{
    public class DeletionAuditDapperActionFilter : DapperActionFilterBase,IDapperActionFilter
    {
        #region IDapperActionFilter 成员

        public void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class
        {
            if (entity is ISoftDelete)
            {
                var record = entity as ISoftDelete;
                record.IsDeleted = true;
            }
        }

        #endregion
    }
}
