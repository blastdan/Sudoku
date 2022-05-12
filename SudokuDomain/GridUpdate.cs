namespace SudokuDomain
{
    public struct GridUpdate
    {
        public GridUpdate(RowIdentifier rowId, ColumnIdentifier columnId, CellRange value)
        {
            RowId = rowId;
            ColumnId = columnId;
            Value = value;
        }

        public RowIdentifier RowId { get; }

        public ColumnIdentifier ColumnId { get; }

        public CellRange Value { get; }
    }
}