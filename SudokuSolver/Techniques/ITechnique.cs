using SudokuDomain;

namespace SudokuSolver.Techniques
{
    internal interface ITechnique
    {
        public CellRange Apply(ColumnIdentifier columnId, RowIdentifier rowId, SudokuGrid grid);
    }
}