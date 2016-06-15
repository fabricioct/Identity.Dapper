﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Dapper
{
    public static class SqlQueryParametersExtensions
    {
        public static List<string> InsertQueryValuesFragment(this List<string> valuesArray, string parameterNotation, IEnumerable<string> propertyNames)
        {
            foreach (var property in propertyNames)
                valuesArray.Add($"{parameterNotation}{property}");

            return valuesArray;
        }

        public static string UpdateQuerySetFragment(this IEnumerable<string> propertyNames, string parameterNotation)
        {
            var setBuilder = new StringBuilder();

            var propertyNamesArray = propertyNames.ToArray();
            for (int i = 0; i < propertyNamesArray.Length; i++)
            {
                var propertyName = propertyNamesArray[i];

                if (i == 0)
                    setBuilder.Append($"SET {propertyName} = {parameterNotation}{propertyName} ");
                else
                    setBuilder.Append($"AND {propertyName} = {parameterNotation}{propertyName} ");
            }

            return setBuilder.ToString();
        }

        public static string SelectFilterWithTableName(this IEnumerable<string> propertyNames, string tableName)
        {
            var propertyNamesArray = propertyNames.ToArray();
            var filterBuilderArray = new List<string>(propertyNamesArray.Length);

            for (int i = 0; i < propertyNamesArray.Length; i++)
                filterBuilderArray.Add($"{tableName}.{propertyNamesArray[i]}");

            return string.Join(", ", filterBuilderArray);
        }
    }
}