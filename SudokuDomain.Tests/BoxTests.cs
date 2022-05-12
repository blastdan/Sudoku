using NUnit.Framework;

namespace SudokuDomain.Tests
{
    [TestFixture]
    public class BoxTests
    {
        [Test]
        [TestCase(ColumnIdentifier.C1, RowIdentifier.R1, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C2, RowIdentifier.R1, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C3, RowIdentifier.R1, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C4, RowIdentifier.R1, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C5, RowIdentifier.R1, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C6, RowIdentifier.R1, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C7, RowIdentifier.R1, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C8, RowIdentifier.R1, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C9, RowIdentifier.R1, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C1, RowIdentifier.R2, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C2, RowIdentifier.R2, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C3, RowIdentifier.R2, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C4, RowIdentifier.R2, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C5, RowIdentifier.R2, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C6, RowIdentifier.R2, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C7, RowIdentifier.R2, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C8, RowIdentifier.R2, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C9, RowIdentifier.R2, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C1, RowIdentifier.R3, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C2, RowIdentifier.R3, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C3, RowIdentifier.R3, BoxIdentifier.B1)]
        [TestCase(ColumnIdentifier.C4, RowIdentifier.R3, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C5, RowIdentifier.R3, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C6, RowIdentifier.R3, BoxIdentifier.B2)]
        [TestCase(ColumnIdentifier.C7, RowIdentifier.R3, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C8, RowIdentifier.R3, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C9, RowIdentifier.R3, BoxIdentifier.B3)]
        [TestCase(ColumnIdentifier.C1, RowIdentifier.R4, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C2, RowIdentifier.R4, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C3, RowIdentifier.R4, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C4, RowIdentifier.R4, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C5, RowIdentifier.R4, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C6, RowIdentifier.R4, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C7, RowIdentifier.R4, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C8, RowIdentifier.R4, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C9, RowIdentifier.R4, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C1, RowIdentifier.R5, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C2, RowIdentifier.R5, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C3, RowIdentifier.R5, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C4, RowIdentifier.R5, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C5, RowIdentifier.R5, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C6, RowIdentifier.R5, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C7, RowIdentifier.R5, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C8, RowIdentifier.R5, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C9, RowIdentifier.R5, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C1, RowIdentifier.R6, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C2, RowIdentifier.R6, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C3, RowIdentifier.R6, BoxIdentifier.B4)]
        [TestCase(ColumnIdentifier.C4, RowIdentifier.R6, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C5, RowIdentifier.R6, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C6, RowIdentifier.R6, BoxIdentifier.B5)]
        [TestCase(ColumnIdentifier.C7, RowIdentifier.R6, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C8, RowIdentifier.R6, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C9, RowIdentifier.R6, BoxIdentifier.B6)]
        [TestCase(ColumnIdentifier.C1, RowIdentifier.R7, BoxIdentifier.B7)]
        [TestCase(ColumnIdentifier.C2, RowIdentifier.R7, BoxIdentifier.B7)]
        [TestCase(ColumnIdentifier.C3, RowIdentifier.R7, BoxIdentifier.B7)]
        [TestCase(ColumnIdentifier.C4, RowIdentifier.R7, BoxIdentifier.B8)]
        [TestCase(ColumnIdentifier.C5, RowIdentifier.R7, BoxIdentifier.B8)]
        [TestCase(ColumnIdentifier.C6, RowIdentifier.R7, BoxIdentifier.B8)]
        [TestCase(ColumnIdentifier.C7, RowIdentifier.R7, BoxIdentifier.B9)]
        [TestCase(ColumnIdentifier.C8, RowIdentifier.R7, BoxIdentifier.B9)]
        [TestCase(ColumnIdentifier.C9, RowIdentifier.R7, BoxIdentifier.B9)]
        public void Constructor_StateUnderTest_ExpectedBehavior(ColumnIdentifier columnId, RowIdentifier rowId, BoxIdentifier expected)
        {
            var Actual = new Box(columnId, rowId);
            Assert.AreEqual(expected, Actual.Id);
        }

        [Test]
        public void WriteValueAndEraseValue_StateUnderTest_ExpectedBehavior([Values] ColumnIdentifier columnId, [Values] RowIdentifier rowId, [Values] CellRange value)
        {
            // Arrange
            var box = new Box(columnId, rowId);

            // Act
            var result = box.WriteValue(
                columnId,
                rowId,
                value);

            // Assert
            Assert.AreEqual(result, value);

            // Try Erase
            var eraseResult = box.EraseValue(columnId, rowId);

            Assert.AreEqual(eraseResult, value);
        }
    }
}