using NUnit.Framework;
using MinesweeperGenerator;
using System;

namespace Tests
{
    public class MinesweeperGridTests
    {
        [Test]
        public void SingleCell()
        {
            bool[,] testGrid = { { true } };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            Assert.AreEqual(1, grid.Height);
            Assert.AreEqual(1, grid.Width);
            Assert.IsTrue(grid[0, 0].HasMine);
            Assert.AreEqual(1, grid[0, 0].NumberOfNearbyMines);
            Assert.Throws<IndexOutOfRangeException>(delegate () { var cell = grid[0, 1]; });
            Assert.Throws<IndexOutOfRangeException>(delegate () { var cell = grid[1, 0]; });
        }

        [Test]
        public void SingleEmptyCell()
        {
            bool[,] testGrid = { { false } };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            Assert.AreEqual(1, grid.Height);
            Assert.AreEqual(1, grid.Width);
            Assert.IsFalse(grid[0, 0].HasMine);
            Assert.AreEqual(0, grid[0, 0].NumberOfNearbyMines);
            Assert.Throws<IndexOutOfRangeException>(delegate () { var cell = grid[0, 1]; });
            Assert.Throws<IndexOutOfRangeException>(delegate () { var cell = grid[1, 0]; });
        }

        [Test]
        public void SingleColumn()
        {
            bool[,] testGrid = {
                { true },
                { false }
            };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            Assert.AreEqual(2, grid.Height);
            Assert.AreEqual(1, grid.Width);
            Assert.IsTrue(grid[0, 0].HasMine);
            Assert.AreEqual(1, grid[0, 0].NumberOfNearbyMines);
            Assert.IsFalse(grid[1, 0].HasMine);
            Assert.AreEqual(1, grid[1, 0].NumberOfNearbyMines);
        }

        [Test]
        public void SingleRow()
        {
            bool[,] testGrid = {
                { true, false }
            };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            Assert.AreEqual(1, grid.Height);
            Assert.AreEqual(2, grid.Width);
            Assert.IsTrue(grid[0, 0].HasMine);
            Assert.AreEqual(1, grid[0, 0].NumberOfNearbyMines);
            Assert.IsFalse(grid[0, 1].HasMine);
            Assert.AreEqual(1, grid[0, 1].NumberOfNearbyMines);
        }

        [Test]
        public void SimpleGrid()
        {
            bool[,] testGrid =
            {
                { true,  false, false },
                { false, false, false },
                { false, false, true  }
            };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            Assert.AreEqual(3, grid.Height);
            Assert.AreEqual(3, grid.Width);

            // Top row.
            Assert.IsTrue(grid[0, 0].HasMine);
            Assert.AreEqual(1, grid[0, 0].NumberOfNearbyMines);
            Assert.IsFalse(grid[0, 1].HasMine);
            Assert.AreEqual(1, grid[0, 1].NumberOfNearbyMines);
            Assert.IsFalse(grid[0, 2].HasMine);
            Assert.AreEqual(0, grid[0, 2].NumberOfNearbyMines);

            // Middle row.
            Assert.IsFalse(grid[1, 0].HasMine);
            Assert.AreEqual(1, grid[1, 0].NumberOfNearbyMines);
            Assert.IsFalse(grid[1, 1].HasMine);
            Assert.AreEqual(2, grid[1, 1].NumberOfNearbyMines);
            Assert.IsFalse(grid[1, 2].HasMine);
            Assert.AreEqual(1, grid[1, 2].NumberOfNearbyMines);

            // Bottom row.
            Assert.IsFalse(grid[2, 0].HasMine);
            Assert.AreEqual(0, grid[2, 0].NumberOfNearbyMines);
            Assert.IsFalse(grid[2, 1].HasMine);
            Assert.AreEqual(1, grid[2, 1].NumberOfNearbyMines);
            Assert.IsTrue(grid[2, 2].HasMine);
            Assert.AreEqual(1, grid[2, 2].NumberOfNearbyMines);

        }

        [Test]
        public void AdjacentMines()
        {
            bool[,] testGrid =
            {
                { true, false },
                { false, true }
            };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            Assert.AreEqual(2, grid.Height);
            Assert.AreEqual(2, grid.Width);

            // Top row.
            Assert.IsTrue(grid[0, 0].HasMine);
            Assert.AreEqual(2, grid[0, 0].NumberOfNearbyMines);
            Assert.IsFalse(grid[0, 1].HasMine);
            Assert.AreEqual(2, grid[0, 1].NumberOfNearbyMines);

            // Bottom row.
            Assert.IsFalse(grid[1, 0].HasMine);
            Assert.AreEqual(2, grid[1, 0].NumberOfNearbyMines);
            Assert.IsTrue(grid[1, 1].HasMine);
            Assert.AreEqual(2, grid[1, 1].NumberOfNearbyMines);
        }
    }
}