using System;

namespace Fanex.BetList.Core.Entities
{
    public struct CellAddress : IEquatable<CellAddress>
    {
        public CellAddress(int rowIndex, int columnIndex)
            : this()
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        public int RowIndex { get; private set; }

        public int ColumnIndex { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is CellAddress other && Equals(other);
        }

        public bool Equals(CellAddress other)
        {
            return RowIndex == other.RowIndex && ColumnIndex == other.ColumnIndex;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + RowIndex.GetHashCode();
                hash = hash * 23 + ColumnIndex.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(CellAddress left, CellAddress right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CellAddress left, CellAddress right)
        {
            return !left.Equals(right);
        }
    }
}