namespace Fanex.BetList.Core.Utils.NPOIExt
{
    using System.Collections.Generic;
    using BetListNPOI.SS.UserModel;

    /// <summary>
    /// Class IRowExtension.
    /// </summary>
    public static class IRowExtension
    {
        public static ICell CreateCell(this IRow row, int colId, ICellStyle cellStyle)
        {
            ICell cell = row.CreateCell(colId);
            cell.CellStyle = cellStyle;
            return cell;
        }

        /// <summary>
        /// Writes the cells.
        /// </summary>
        /// <param name="row">The row object.</param>
        /// <param name="colId">The column identifier.</param>
        /// <param name="values">The values.</param>
        public static void WriteCells(this IRow row, int colId, params object[] values)
        {
            int id = colId;
            var cellValues = values;

            if (cellValues.Length == 1 && cellValues[0] is List<object>)
            {
                cellValues = ((List<object>)cellValues[0]).ToArray();
            }

            foreach (var value in cellValues)
            {
                row.GetCell(id).SetCellValue(value);
                id++;
            }
        }
    }
}