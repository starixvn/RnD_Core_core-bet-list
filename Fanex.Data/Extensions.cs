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
//  <change who="Edward.Nguyen" date="2013.2.7">Remove get out put parameter reader</change>
// </history>
namespace Fanex.Data
{
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

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
        /// Gets the name of Sql parameter without prefix @
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>parameter name</returns>
        public static string GetLowerName(this SqlParameter param)
        {
            return param.GetName().ToLowerInvariant();
        }

        /// <summary>
        /// Gets the name of Sql parameter without prefix @
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetName(this SqlParameter param)
        {
            return param.ParameterName.TrimStart('@');
        }
    }
}
