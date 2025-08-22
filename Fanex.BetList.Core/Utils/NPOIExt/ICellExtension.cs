namespace Fanex.BetList.Core.Utils.NPOIExt
{
    using System;
    using BetListNPOI.SS.UserModel;

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
                if (value is string v)
                {
                    cell.SetCellValue(v);
                }
                else if (value is int i)
                {
                    cell.SetCellValue(i);
                }
                else if (value is long l)
                {
                    cell.SetCellValue(l);
                }
                else if (value is double d)
                {
                    cell.SetCellValue(d);
                }
                else if (value is decimal dec)
                {
                    cell.SetCellValue(dec);
                }
                else if (value is bool b)
                {
                    cell.SetCellValue(b);
                }
                else if (value is DateTime dt)
                {
                    cell.SetCellValue(dt);
                }
                else if (value is IRichTextString irt)
                {
                    cell.SetCellValue(irt);
                }
                else
                {
                    cell.SetCellValue("Undefined");
                }
            }
        }
    }
}