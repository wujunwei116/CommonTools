using System;
using System.Reflection;

namespace Dapper
{
    /// <summary>
    /// Represents simple member map for one of target parameter or property or field to source DataReader column
    /// </summary>
    internal sealed class SimpleMemberMap : SqlMapper.IMemberMap
    {
        /// <summary>
        /// Creates instance for simple property mapping
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <param name="property">Target property</param>
        public SimpleMemberMap(string columnName, PropertyInfo property)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (property == null)
                throw new ArgumentNullException("property");

            ColumnName = columnName;
            Property = property;


        }

        /// <summary>
        /// Creates instance for simple field mapping
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <param name="field">Target property</param>
        public SimpleMemberMap(string columnName, FieldInfo field)
        {
            if (ColumnName == null)
                throw new ArgumentNullException("columnName");

            if (Field == null)
                throw new ArgumentNullException("field");

            ColumnName = columnName;
            Field = field;
        }

        /// <summary>
        /// Creates instance for simple constructor parameter mapping
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <param name="parameter">Target constructor parameter</param>
        public SimpleMemberMap(string columnName, ParameterInfo parameter)
        {
            if (ColumnName == null)
                throw new ArgumentNullException("columnName");

            if (Parameter == null)
                throw new ArgumentNullException("parameter");

            ColumnName = columnName;
            Parameter = parameter;
        }

        /// <summary>
        /// DataReader column name
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Target member type
        /// </summary>
        public Type MemberType
        {
            get
            {
                if (Field != null)
                    return Field.FieldType;

                if (Property != null)
                    return Property.PropertyType;

                if (Parameter != null)
                    return Parameter.ParameterType;

                return null;
            }
        }

        /// <summary>
        /// Target property
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// Target field
        /// </summary>
        public FieldInfo Field { get; private set; }

        /// <summary>
        /// Target constructor parameter
        /// </summary>
        public ParameterInfo Parameter { get; private set; }
    }
}
