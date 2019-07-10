using System;

namespace MinesweeperGenerator
{
    /// <summary>
    /// Minesweeper Grid class.
    /// 
    /// Accepts a "grid specification" that consists of a 2-dimensional
    /// boolean array. 'true' indicates the presence of a mine at those
    /// co-ordinates and 'false' indicates an empty cell.
    /// 
    /// The MinesweeperGrid class will read the specification and generate a
    /// cell grid, each one holding information about whether a mine is present
    /// and the number of mines present in adjacent cells.
    /// </summary>
    public class MinesweeperGrid
    {
        const int ROW_DIMENSION = 0;
        const int COLUMN_DIMENSION = 1;

        /// <summary>
        /// Internal cell implementation.
        /// </summary>
        private class Cell : ICell
        {
            public Cell()
            {
                _hasMine = false;
                _numberOfNearbyMines = 0;
            }

            public bool HasMine
            {
                get { return _hasMine; }
            }

            public int NumberOfNearbyMines
            {
                get { return _numberOfNearbyMines; }
            }

            public void IncrementNumberOfNearbyMines()
            {
                ++_numberOfNearbyMines;
            }

            public void SetHasMine()
            {
                _hasMine = true;
            }

            private bool _hasMine;
            private int _numberOfNearbyMines;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gridSpecification">
        /// Specification that defines the minesweeper grid.
        /// </param>
        public MinesweeperGrid(bool[,] gridSpecification)
        {
            InitializeGrid(gridSpecification.GetLength(ROW_DIMENSION), gridSpecification.GetLength(COLUMN_DIMENSION));
            ParseGridSpecification(gridSpecification);
        }

        /// <summary>
        /// Initialize an empty grid.
        /// </summary>
        /// <param name="height">Height of the grid.</param>
        /// <param name="width">Width of the grid.</param>
        private void InitializeGrid(int height, int width)
        {
            _grid = new Cell[height, width];

            for (int row = 0; row < height; ++row)
            {
                for (int column = 0; column < width; ++column)
                {
                    _grid[row, column] = new Cell();
                }
            }
        }

        /// <summary>
        /// Parse the given grid specification and load the grid contents.
        /// </summary>
        /// <param name="gridSpecification">The grid specification.</param>
        private void ParseGridSpecification(bool[,] gridSpecification)
        {
            int height = gridSpecification.GetLength(ROW_DIMENSION);
            int width = gridSpecification.GetLength(COLUMN_DIMENSION);

            for (int row = 0; row < height; ++row)
            {
                for (int column = 0; column < width; ++column)
                {
                    SetCell(row, column, gridSpecification[row, column]);
                }
            }
        }

        /// <summary>
        /// Set the contents of a cell.
        /// </summary>
        /// <param name="row">row index.</param>
        /// <param name="column">column index.</param>
        /// <param name="hasMine">Does this cell contain a mine?</param>
        private void SetCell(int row, int column, bool hasMine)
        {
            Cell cell = _grid[row, column];

            if (hasMine)
            {
                cell.SetHasMine();
                IncrementNearbyCells(row, column);
            }
        }

        /// <summary>
        /// Increment the number of nearby mines for cells adjacent to the given
        /// coordinates.
        /// </summary>
        /// <param name="row">row index.</param>
        /// <param name="column">column index.</param>
        private void IncrementNearbyCells(int row, int column)
        {
            // Increment the number of nearby mines for adjacent cells.
            for (int adjacentRow = row - 1; adjacentRow <= row + 1; ++adjacentRow)
            {
                // Bounds check.
                if ((adjacentRow < 0) || (adjacentRow >= _grid.GetLength(ROW_DIMENSION)))
                    continue;
                
                for (int adjacentColumn = column - 1; adjacentColumn <= column + 1; ++adjacentColumn)
                {
                    // Bounds check again.
                    if ((adjacentColumn < 0) || (adjacentColumn >= _grid.GetLength(COLUMN_DIMENSION)))
                        continue;

                    _grid[adjacentRow, adjacentColumn].IncrementNumberOfNearbyMines();
                }
            }
        }

        /// <summary>
        /// Get the height of the grid (number of rows).
        /// </summary>
        public int Height { get { return _grid.GetLength(ROW_DIMENSION); } }

        /// <summary>
        /// Get the width of the grid (number of columns).
        /// </summary>
        public int Width { get { return _grid.GetLength(COLUMN_DIMENSION); } }

        /// <summary>
        /// Get a specific cell from the grid.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <returns>An ICell instance. Never returns null.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Invalid row/column specified.
        /// </exception>
        public ICell this[int row, int column]
        {
            get { return _grid[row, column]; }
        }

        private Cell[,] _grid;
    }
}
