namespace Fanex.BetList.Core.Entities
{
    public struct CellAddress
    {
        public CellAddress(int rowIndex, int columnIndex)
            : this()
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        public int RowIndex { get; private set; }

        public int ColumnIndex { get; private set; }
    }
}