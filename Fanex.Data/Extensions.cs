// <copyright file="Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  Data extension methods
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
//  <change who="Anders" date="2011.12.14">Add function GetFirstOrDefault for class IEnumerable</change>
// </history>
namespace Fanex.Data
{
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Collections;
    
    /// <summary>
    /// This class extend some functions DBCommand and SqlParameter
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Sql return parameter name
        /// </summary>
        private const string ReturnValueParamName = "@RETURN_VALUE";

        /// <summary>
        /// Determines whether [has output parameter] [the specified db command].
        /// </summary>
        /// <param name="databaseCmd">The db command.</param>
        /// <returns>
        ///   <c>true</c> if [has output parameter] [the specified db command]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasOutputParam(this DbCommand databaseCmd)
        {
            foreach (SqlParameter param in databaseCmd.Parameters)
            {
                if (param.IsNotReturnValue()
                   && param.Direction != ParameterDirection.Input)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is not return value] [the specified parameter].
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if [is not return value] [the specified parameter]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotReturnValue(this SqlParameter param)
        {
            return !param.ParameterName.Equals(ReturnValueParamName);
        }

        /// <summary>
        /// Gets the name of sql parameter without prefix @
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>parameter name</returns>
        public static string GetLowerName(this SqlParameter param)
        {
            return param.GetName().ToLowerInvariant();
        }
        
        /// <summary>
        /// Gets the name of sql parameter without prefix @
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetName(this SqlParameter param)
        {
            return param.ParameterName.TrimStart('@');
        }

        /// <summary>
        /// For get out put parameter from data reader
        /// </summary>
        /// <param name="paramaterValues"></param>
        public static void GetReaderOutParam(
            this ObjectDb objectDb,
            Hashtable paramaterValues)
        {
            //AssignDynamicParamsValue(DbCommand, paramaterValues);
            //IDataReader reader= Database.ExecuteReader(DbCommand);
            foreach (SqlParameter paramItem in objectDb.DbCommand.Parameters)
            {
                if (paramItem.Direction == ParameterDirection.Output || paramItem.Direction == ParameterDirection.InputOutput)
                {
                    object value = objectDb.GetParameterValue(paramItem.GetLowerName());
                    paramaterValues[paramItem.GetLowerName()] = value;
                }
            }
            //return reader;
        }
    }
}
