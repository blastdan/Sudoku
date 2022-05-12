using System.Diagnostics.CodeAnalysis;
using System.Reactive.Subjects;

namespace SudokuDomain
{
    public class SudokuGrid
    {
        private Subject<GridUpdate> _gridUpdateSubject = new Subject<GridUpdate>();

        public SudokuGrid()
        {
            this.Completed = false;
        }

        public bool Completed { get; set; }

        public bool IsSolved {  get
            {
                return
                this.Rows.All(r => r.Values.Count == 9) &&
                this.Columns.All(c => c.Values.Count == 9) &&
                this.Boxes.All(b => b.Values.Count == 9);
            } 
        }

        public HashSet<Row> Rows { get; } = new HashSet<Row>()
        {
            new Row(RowIdentifier.R1),
            new Row(RowIdentifier.R2),
            new Row(RowIdentifier.R3),
            new Row(RowIdentifier.R4),
            new Row(RowIdentifier.R5),
            new Row(RowIdentifier.R6),
            new Row(RowIdentifier.R7),
            new Row(RowIdentifier.R8),
            new Row(RowIdentifier.R9),
        };

        public HashSet<Column> Columns { get; } = new HashSet<Column>()
        {
            new Column(ColumnIdentifier.C1),
            new Column(ColumnIdentifier.C2),
            new Column(ColumnIdentifier.C3),
            new Column(ColumnIdentifier.C4),
            new Column(ColumnIdentifier.C5),
            new Column(ColumnIdentifier.C6),
            new Column(ColumnIdentifier.C7),
            new Column(ColumnIdentifier.C8),
            new Column(ColumnIdentifier.C9),
        };

        public HashSet<Box> Boxes { get; } = new HashSet<Box>()
        {
            new Box(BoxIdentifier.B1),
            new Box(BoxIdentifier.B2),
            new Box(BoxIdentifier.B3),
            new Box(BoxIdentifier.B4),
            new Box(BoxIdentifier.B5),
            new Box(BoxIdentifier.B6),
            new Box(BoxIdentifier.B7),
            new Box(BoxIdentifier.B8),
            new Box(BoxIdentifier.B9),
        };

        public SudokuGrid Set(Dictionary<(ColumnIdentifier, RowIdentifier), CellRange> puzzle)
        {
            if (puzzle is null)
            {
                throw new ArgumentNullException(nameof(puzzle));
            }

            foreach (var cell in puzzle)
            {
                this.WriteValue(cell.Key.Item2, cell.Key.Item1, cell.Value);
            }

            return this;
        }

        public IObservable<GridUpdate> GridUpdates
        {
            get
            {
                return _gridUpdateSubject;
            }
        }

        public CellRange WriteValue(RowIdentifier rowId, ColumnIdentifier columnId, CellRange value)
        {
            this.Rows.TryGetValue(rowId, out Row foundRow);
            this.Columns.TryGetValue(columnId, out Column foundColumn);
            this.Boxes.TryGetValue(new Box(columnId, rowId).Id, out Box foundBox);

            var columnValue = foundColumn.WriteValue(rowId, value);
            var rowValue = foundRow.WriteValue(columnId, value);
            var boxValue = foundBox.WriteValue(columnId, rowId, value);

            if (columnValue != rowValue)
            {
                // Log
            }

            this._gridUpdateSubject.OnNext(new GridUpdate(rowId, columnId, value));

            return rowValue;
        }

        public CellRange ReadValue(RowIdentifier rowId, ColumnIdentifier columnIdentifier)
        {
            this.Rows.TryGetValue(rowId, out Row foundRow);
            return foundRow.ReadValue(columnIdentifier);
        }
    }
}