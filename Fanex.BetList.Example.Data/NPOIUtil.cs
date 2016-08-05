namespace Fanex.BetList.Example.Data
{
    using NPOI.SS.UserModel;

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