﻿
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Acon.Dapper.Extensions
{
    public static class SortingExtensions
    {
        public static List<ISort> ToSortable<T>(this Expression<Func<T, object>>[] sortingExpression, bool ascending = true)
        {
            Check.NotNullOrEmpty(sortingExpression, "sortingExpression");

            var sortList = new List<ISort>();
            sortingExpression.ToList().ForEach(sortExpression =>
            {
                MemberInfo sortProperty = ReflectionHelper.GetProperty(sortExpression);
                sortList.Add(new Sort { Ascending = ascending, PropertyName = sortProperty.Name });
            });

            return sortList;
        }
    }
}
