using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acon.Collections.Extensions;
using Acon.Extensions;
namespace Acon
{
    public class Check
    {
        public static T NotNull<T>(T value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentException(parameterName + " 不能为空!", parameterName);
            }

            return value;
        }
        public static string NotNullOrEmpty(string value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(parameterName + " 不能为空!", parameterName);
            }

            return value;
        }

        public static string NotNullOrWhiteSpace(string value, string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException(parameterName + " 不能为空!", parameterName);
            }

            return value;
        }

        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(parameterName + " 不能为空!", parameterName);
            }

            return value;
        }

        public static int NotEmpty(int value, string parameterName)
        {
            if (value <= 0)
            {
                throw new ArgumentException(parameterName + " 必须大于0!", parameterName);
            }
            return value;
        }
    }
}
