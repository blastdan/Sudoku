using System.Collections.Concurrent;

namespace SudokuDomain
{
    public struct Row : IEquatable<Row>
    {
        private readonly RowIdentifier id;
        private ConcurrentDictionary<ColumnIdentifier, CellRange> cellValues;

        public Row(RowIdentifier id)
        {
            this.id = id;
            this.cellValues = new ConcurrentDictionary<ColumnIdentifier, CellRange>();
        }

        public ICollection<CellRange> Values { get => this.cellValues.Values; }

        public CellRange WriteValue(ColumnIdentifier id, CellRange value)
        {
            return this.cellValues.AddOrUpdate(id, value, (k, v) => value);
        }

        public CellRange EraseValue(ColumnIdentifier id)
        {
            this.cellValues.Remove(id, out CellRange erasedValue);

            return erasedValue;
        }

        public CellRange ReadValue(ColumnIdentifier id)
        {
            return this.cellValues.GetValueOrDefault(id, default(CellRange));
        }

        public static implicit operator RowIdentifier(Row r) => r.id;

        public static implicit operator Row(RowIdentifier id) => new Row(id);

        #region Equals Override

        public override bool Equals(object? obj)
        {
            return obj is Row row && Equals(row);
        }

        public bool Equals(Row other)
        {
            return id == other.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id);
        }

        public static bool operator ==(Row left, Row right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Row left, Row right)
        {
            return !(left == right);
        }

        #endregion Equals Override
    }
}