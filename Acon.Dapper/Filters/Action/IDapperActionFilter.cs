using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Dapper.Filters.Action
{
   public interface IDapperActionFilter
    {
        void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class;
    }
}
