// ---------------------------------------------------------------------------------
// <copyright file="NPOIUtil.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.12">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Example.Data
{
    using NPOI.SS.UserModel;

    /// <summary>
    /// Class NPOI Utilities.
    /// </summary>
    internal static class NPOIUtil
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="cell">The ICell object.</param>
        /// <param name="dataFormatter">The data formatter.</param>
        /// <param name="formulaEvaluator">The formula evaluator.</param>
        /// <returns>Value string.</returns>
        public static string GetValue(ICell cell, DataFormatter dataFormatter, IFormulaEvaluator formulaEvaluator)
        {
            string ret = string.Empty;

            if (null == cell)
            {
                return ret;
            }

            ret = dataFormatter.FormatCellValue(cell, formulaEvaluator);

            // remove line break
            return ret.Replace("\n", " ");
        }
    }
}