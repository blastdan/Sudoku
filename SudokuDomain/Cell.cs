namespace SudokuDomain
{
    internal class Cell : IEquatable<Cell?>
    {
        private CellRange value;

        public Cell()
        {
            this.value = CellRange.None;
        }

        public Cell(CellRange value)
        {
            this.value = value;
        }

        #region Equals Override

        public override bool Equals(object? obj)
        {
            return Equals(obj as Cell);
        }

        public bool Equals(Cell? other)
        {
            return other != null &&
                   value == other.value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value);
        }

        public static bool operator ==(Cell? left, Cell? right)
        {
            return EqualityComparer<Cell>.Default.Equals(left, right);
        }

        public static bool operator !=(Cell? left, Cell? right)
        {
            return !(left == right);
        }

        #endregion Equals Override
    }
}