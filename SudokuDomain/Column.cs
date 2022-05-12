using System.Collections.Concurrent;

namespace SudokuDomain
{
    public struct Column : IEquatable<Column>
    {
        private readonly ColumnIdentifier id;
        private ConcurrentDictionary<RowIdentifier, CellRange> cellValues;

        public Column(ColumnIdentifier id)
        {
            this.id = id;
            this.cellValues = new ConcurrentDictionary<RowIdentifier, CellRange>();
        }

        public ICollection<CellRange> Values { get => this.cellValues.Values; }

        public CellRange WriteValue(RowIdentifier id, CellRange value)
        {
            return this.cellValues.AddOrUpdate(id, value, (k, v) => value);
        }

        public CellRange EraseValue(RowIdentifier id)
        {
            this.cellValues.Remove(id, out CellRange erasedValue);

            return erasedValue;
        }

        public CellRange ReadValue(RowIdentifier id)
        {
            return this.cellValues.GetValueOrDefault(id, default(CellRange));
        }

        #region Equals Override

        public override bool Equals(object? obj)
        {
            return obj is Column column && Equals(column);
        }

        public bool Equals(Column other)
        {
            return id == other.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id);
        }

        public static implicit operator ColumnIdentifier(Column r) => r.id;

        public static implicit operator Column(ColumnIdentifier id) => new Column(id);

        public static bool operator ==(Column left, Column right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Column left, Column right)
        {
            return !(left == right);
        }

        #endregion Equals Override
    }
}