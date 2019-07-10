namespace MinesweeperGenerator
{
    /// <summary>
    /// Defines the interface for a cell within a minesweeper grid. This
    /// interface exists because it allows us to expose a read-only interface
    /// for the cell, while the MinesweeperGrid class is allowed to create an
    /// internal implementation of the interface that can create the ICells
    /// however it wishes.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Does this cell contain a mine?
        /// </summary>
        /// <returns>
        /// true if a mine is present in the cell, otherwise false.
        /// </returns>
        bool HasMine { get; }

        /// <summary>
        /// Get the number of nearby mines.
        /// </summary>
        /// <returns>
        /// A positive integer between zero and N that represents the number of
        /// mines in surrounding cells.
        /// </returns>
        int NumberOfNearbyMines { get; }
    }
}