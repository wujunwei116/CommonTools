using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Dapper.Filters.Action
{
   public class CreationAuditDapperActionFilter : DapperActionFilterBase, IDapperActionFilter
    {
       #region IDapperActionFilter 成员

       public void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class
       {
           CheckAndSetId(entity);
       }

       #endregion
    }
}
