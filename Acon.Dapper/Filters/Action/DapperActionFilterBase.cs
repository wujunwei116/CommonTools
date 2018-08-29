using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acon.Dapper.Filters.Action
{
   public abstract class DapperActionFilterBase
    {
        protected virtual void CheckAndSetId(object entityAsObj)
        {
            //var entity = entityAsObj as IEntity<Guid>;

            //if (entity != null && entity.Id == Guid.Empty)
            //{
            //    Type entityType = entityAsObj.GetType();
            //    PropertyInfo idProperty = entityType.GetProperty("Id");
            //    var dbGeneratedAttr = ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
            //    if (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
            //    {
            //        entity.Id = GuidGenerator.Create();
            //    }
            //}


        }
    }
}
