using NUnit.Framework;
using SudokuDomain;
using SudokuSolver.Techniques;
using System;
using System.Collections.Generic;

namespace SudokuSolver.Tests
{
    public class Tests
    {
        Dictionary<(ColumnIdentifier, RowIdentifier), CellRange> veryEasyPuzzle = new Dictionary<(ColumnIdentifier, RowIdentifier), CellRange>()
        {
            {(ColumnIdentifier.C3, RowIdentifier.R1), CellRange.Nine },
            {(ColumnIdentifier.C4, RowIdentifier.R1), CellRange.Six },
            {(ColumnIdentifier.C6, RowIdentifier.R1), CellRange.Two },
            {(ColumnIdentifier.C7, RowIdentifier.R1), CellRange.One },
            {(ColumnIdentifier.C9, RowIdentifier.R1), CellRange.Five },

            {(ColumnIdentifier.C1, RowIdentifier.R2), CellRange.Five },
            {(ColumnIdentifier.C2, RowIdentifier.R2), CellRange.Seven },
            {(ColumnIdentifier.C4, RowIdentifier.R2), CellRange.Three },
            {(ColumnIdentifier.C5, RowIdentifier.R2), CellRange.Four },
            {(ColumnIdentifier.C8, RowIdentifier.R2), CellRange.Six },

            {(ColumnIdentifier.C2, RowIdentifier.R3), CellRange.Two },
            {(ColumnIdentifier.C6, RowIdentifier.R3), CellRange.Five },
            {(ColumnIdentifier.C7, RowIdentifier.R3), CellRange.Four },
            {(ColumnIdentifier.C8, RowIdentifier.R3), CellRange.Three },
            {(ColumnIdentifier.C9, RowIdentifier.R3), CellRange.Seven },

            {(ColumnIdentifier.C1, RowIdentifier.R4), CellRange.Nine },
            {(ColumnIdentifier.C3, RowIdentifier.R4), CellRange.Three },
            {(ColumnIdentifier.C5, RowIdentifier.R4), CellRange.Five },
            {(ColumnIdentifier.C6, RowIdentifier.R4), CellRange.Seven },

            {(ColumnIdentifier.C1, RowIdentifier.R5), CellRange.Four },
            {(ColumnIdentifier.C3, RowIdentifier.R5), CellRange.Seven },
            {(ColumnIdentifier.C6, RowIdentifier.R5), CellRange.Six },
            {(ColumnIdentifier.C7, RowIdentifier.R5), CellRange.Two },
            {(ColumnIdentifier.C8, RowIdentifier.R5), CellRange.Five },
            {(ColumnIdentifier.C9, RowIdentifier.R5), CellRange.Nine },

            {(ColumnIdentifier.C2, RowIdentifier.R6), CellRange.Eight },
            {(ColumnIdentifier.C4, RowIdentifier.R6), CellRange.Four },
            {(ColumnIdentifier.C5, RowIdentifier.R6), CellRange.Nine },
            {(ColumnIdentifier.C7, RowIdentifier.R6), CellRange.Six },
            {(ColumnIdentifier.C8, RowIdentifier.R6), CellRange.Seven },

            {(ColumnIdentifier.C4, RowIdentifier.R7), CellRange.Seven },
            {(ColumnIdentifier.C5, RowIdentifier.R7), CellRange.Two },
            {(ColumnIdentifier.C7, RowIdentifier.R7), CellRange.Three },
            {(ColumnIdentifier.C8, RowIdentifier.R7), CellRange.Nine },
            {(ColumnIdentifier.C9, RowIdentifier.R7), CellRange.Six },

            {(ColumnIdentifier.C1, RowIdentifier.R8), CellRange.Seven },
            {(ColumnIdentifier.C2, RowIdentifier.R8), CellRange.Three },
            {(ColumnIdentifier.C3, RowIdentifier.R8), CellRange.Four },
            {(ColumnIdentifier.C4, RowIdentifier.R8), CellRange.One },
            {(ColumnIdentifier.C6, RowIdentifier.R8), CellRange.Nine },

            {(ColumnIdentifier.C2, RowIdentifier.R9), CellRange.Nine },
            {(ColumnIdentifier.C3, RowIdentifier.R9), CellRange.Two },
            {(ColumnIdentifier.C5, RowIdentifier.R9), CellRange.Eight },
            {(ColumnIdentifier.C8, RowIdentifier.R9), CellRange.Four },
            {(ColumnIdentifier.C9, RowIdentifier.R9), CellRange.One },
        };

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Attempt()
        {
            var grid = new SudokuGrid();
            grid.Set(this.veryEasyPuzzle);

            var results = new List<CellRange>();
            var solvedCell = new SolvedCell();

            do
            {
                results.Clear();

                foreach (ColumnIdentifier column in (ColumnIdentifier[])Enum.GetValues(typeof(ColumnIdentifier)))
                {
                    foreach (RowIdentifier row in (RowIdentifier[])Enum.GetValues(typeof(RowIdentifier)))
                    {
                        if (column == ColumnIdentifier.None || row == RowIdentifier.None)
                        {
                            continue;
                        }

                        var result = solvedCell.Apply(column, row, grid);
                        results.Add(result);

                        if(result != CellRange.None)
                        {
                            grid.WriteValue(row, column, result);
                        }
                    }
                }
            }
            while (results.TrueForAll(r => r == CellRange.None));

            Assert.IsTrue(grid.IsSolved);
        }
    }
}