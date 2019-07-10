using NUnit.Framework;
using MinesweeperGenerator;
using System;

namespace Tests
{
    public class ProgramTests
    {
        [Test]
        public void PrintGridTest1()
        {
            bool[,] testGrid = { { true } };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            var gridText = Program.PrintGrid(grid).ToString();
            var expectedOutput = "+---+\n| * |\n+---+\n";

            Assert.AreEqual(expectedOutput, gridText);

        }

        [Test]
        public void PrintGridTest2()
        {
            bool[,] testGrid = {
                { true },
                { false }
            };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            var gridText = Program.PrintGrid(grid).ToString();
            var expectedOutput = "+---+\n| * |\n+---+\n| 1 |\n+---+\n";

            Assert.AreEqual(expectedOutput, gridText);

        }

        [Test]
        public void PrintGridTest3()
        {
            bool[,] testGrid =
            {
                { true,  false, false },
                { false, false, false },
                { false, false, true  }
            };

            MinesweeperGrid grid = new MinesweeperGrid(testGrid);

            var gridText = Program.PrintGrid(grid).ToString();
            var expectedOutput = "+---+---+---+\n| * | 1 | 0 |\n+---+---+---+\n| 1 | 2 | 1 |\n+---+---+---+\n| 0 | 1 | * |\n+---+---+---+\n";

            Assert.AreEqual(expectedOutput, gridText);

        }

        [Test]
        public void GenerateSuccessfulGridSpecification()
        {
            var sr = new System.IO.StringReader(
                @"3 3
*..
...
..*");
            var gridSpecification = Program.GenerateGridSpecification(sr);
            bool[,] expectedResult =
            {
                { true,  false, false },
                { false, false, false },
                { false, false, true  }
            };

            Assert.AreEqual(expectedResult, gridSpecification);
        }

        [Test]
        public void TryGenerateBadGridSpecification1()
        {
            // String rows are inconsistent (row 2 has only 2 characters, should be three).
            var sr = new System.IO.StringReader(
                @"3 3
*..
..
..*");
            Assert.Throws<Exception>(delegate () { Program.GenerateGridSpecification(sr); });
        }

        [Test]
        public void TryGenerateBadGridSpecification2()
        {
            // Not enough rows.
            var sr = new System.IO.StringReader(
                @"3 3
*..
...");
            Assert.Throws<Exception>(delegate () { Program.GenerateGridSpecification(sr); });
        }

        [Test]
        public void TryGenerateBadGridSpecification3()
        {
            // Invalid character.
            var sr = new System.IO.StringReader(
                @"3 3
*..
...
..X");
            Assert.Throws<Exception>(delegate () { Program.GenerateGridSpecification(sr); });
        }
    }
}
