using SudokuDomain;

namespace SudokuSolver.Techniques
{
    public class SolvedCell : ITechnique
    {
        public CellRange Apply(ColumnIdentifier columnId, RowIdentifier rowId, SudokuGrid grid)
        {
            var initialCellValue = grid.ReadValue(rowId, columnId);
            if (initialCellValue != CellRange.None)
            {
                return CellRange.None;
            }    


            var boxId = new Box(columnId, rowId).Id;
            grid.Rows.TryGetValue(rowId, out Row row);
            grid.Columns.TryGetValue(columnId, out Column column);
            grid.Boxes.TryGetValue(boxId, out Box box);
            var nones = new CellRange[1] { CellRange.None };
            var allValues = (CellRange[])Enum.GetValues(typeof(CellRange));

            var remainingValues = allValues.Except(row.Values)
                                           .Except(box.Values)
                                           .Except(nones)
                                           .ToArray();

            if (remainingValues.Length != 1)
            {
                return CellRange.None;
            }

            return remainingValues[0];
        }
    }
}