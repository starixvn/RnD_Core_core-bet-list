namespace Fanex.BetList.Core.Utils.NPOIExt
{
    using System;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extension for ICell.
    /// </summary>
    public static class ICellExtension
    {
        /// <summary>
        /// Sets the cell value.
        /// </summary>
        /// <param name="cell">The cell which will be assigned the value.</param>
        /// <param name="value">The value object to set.</param>
        public static void SetCellValue(this ICell cell, object value)
        {
            if (value == null)
            {
                cell.SetCellValue("0");
            }
            else
            {
                if (value is string)
                {
                    // cell.SetCellValue(value);
                    cell.SetCellValue((string)value);
                }
                else if (value is int)
                {
                    cell.SetCellValue((int)value);
                }
                else if (value is long)
                {
                    cell.SetCellValue((long)value);
                }
                else if (value is double)
                {
                    cell.SetCellValue((double)value);
                }
                else if (value is decimal)
                {
                    cell.SetCellValue((decimal)value);
                }
                else if (value is bool)
                {
                    cell.SetCellValue((bool)value);
                }
                else if (value is DateTime)
                {
                    cell.SetCellValue((DateTime)value);
                }
                else if (value is IRichTextString)
                {
                    cell.SetCellValue((IRichTextString)value);

                    // cell.SetCellValue(value);
                }
                else
                {
                    cell.SetCellValue("Undefined");
                }
            }
        }
    }
}