using System.Collections.Concurrent;

namespace SudokuDomain
{
    public struct Box
    {
        private readonly BoxIdentifier id;
        private ConcurrentDictionary<(ColumnIdentifier, RowIdentifier), CellRange> cellValues = new ConcurrentDictionary<(ColumnIdentifier, RowIdentifier), CellRange>();

        public Box(BoxIdentifier id) : this()
        {
            this.id = id;
        }

        public Box(ColumnIdentifier columnId, RowIdentifier rowId) : this()
        {
            var boxId = (rowId, columnId) switch
            {
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C3 && r <= RowIdentifier.R3 => BoxIdentifier.B1,
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C3 && r <= RowIdentifier.R6 => BoxIdentifier.B4,
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C3 && r <= RowIdentifier.R9 => BoxIdentifier.B7,
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C6 && r <= RowIdentifier.R3 => BoxIdentifier.B2,
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C6 && r <= RowIdentifier.R6 => BoxIdentifier.B5,
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C6 && r <= RowIdentifier.R9 => BoxIdentifier.B8,
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C9 && r <= RowIdentifier.R3 => BoxIdentifier.B3,
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C9 && r <= RowIdentifier.R6 => BoxIdentifier.B6,
                (rowId: var r, columnId: var c) when c <= ColumnIdentifier.C9 && r <= RowIdentifier.R9 => BoxIdentifier.B9,
                (_, _) => BoxIdentifier.None
            };

            this.id = boxId;
        }

        public BoxIdentifier Id { get => this.id; }

        public ICollection<CellRange> Values { get => this.cellValues.Values; }

        public CellRange WriteValue(ColumnIdentifier columnId, RowIdentifier rowId, CellRange value)
        {
            return this.cellValues.AddOrUpdate((columnId, rowId), value, (key, value) => value);
        }

        public CellRange EraseValue(ColumnIdentifier columnId, RowIdentifier rowId)
        {
            this.cellValues.Remove((columnId, rowId), out CellRange erasedValue);

            return erasedValue;
        }

        public CellRange ReadValue(ColumnIdentifier columnId, RowIdentifier rowId)
        {
            return this.cellValues.GetValueOrDefault((columnId, rowId), default(CellRange));
        }

        #region Equals Override

        public override bool Equals(object? obj)
        {
            return obj is Box column && Equals(column);
        }

        public bool Equals(Box other)
        {
            return id == other.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id);
        }

        public static implicit operator BoxIdentifier(Box r) => r.id;

        public static implicit operator Box(BoxIdentifier id) => new Box(id);

        public static bool operator ==(Box left, Box right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Box left, Box right)
        {
            return !(left == right);
        }

        #endregion Equals Override
    }
}