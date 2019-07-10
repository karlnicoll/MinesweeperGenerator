using System;
using System.IO;
using System.Text;

namespace MinesweeperGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            // File path could be read from a command line arg.
            var fileReader = new StreamReader("../../../test.txt");

            // Create the grid specification by reading the file.
            var gridSpecification = GenerateGridSpecification(fileReader);

            // Create the grid from the specification.
            var minesweeperGrid = new MinesweeperGrid(gridSpecification);

            // Print the contents of the grid.
            var gridText = PrintGrid(minesweeperGrid);
            Console.WriteLine(gridText);
        }

        /// <summary>
        /// Print the grid.
        /// </summary>
        /// <param name="minesweeperGrid">Grid to print.</param>
        public static StringWriter PrintGrid(MinesweeperGrid minesweeperGrid)
        {
            StringWriter stringWriter = new StringWriter();
            for (int row = 0; row < minesweeperGrid.Height; ++row)
            {
                stringWriter.Write(GenerateSeparatorLine(minesweeperGrid));
                stringWriter.Write("|");
                for (int column = 0; column < minesweeperGrid.Width; ++column)
                {
                    var cell = minesweeperGrid[row, column];

                    if (cell.HasMine)
                        stringWriter.Write(" * ");
                    else
                        stringWriter.Write(" {0} ", cell.NumberOfNearbyMines);

                    stringWriter.Write("|");
                }

                // Write a line between rows.
                stringWriter.Write("\n");
            }

            stringWriter.Write(GenerateSeparatorLine(minesweeperGrid));

            return stringWriter;
        }

        /// <summary>
        /// Generate a grid specification from the given text stream.
        /// </summary>
        /// <param name="reader">text reader.</param>
        /// <returns>A valid grid specification.</returns>
        public static bool[,] GenerateGridSpecification(TextReader reader)
        {
            ReadDimensions(reader, out var rows, out var columns);

            bool[,] result = new bool[rows, columns];

            int ch;
            int row = 0;
            int column = 0;
            while ((ch = reader.Read()) != -1)
            {
                if (row >= rows)
                    throw new Exception("Too many rows in the grid specification.");

                if (ch == '\r')
                    continue;

                if (ch == '\n')
                {
                    if (column < columns)
                        throw new Exception($"Not enough columns in the grid specification (row: {row}).");

                    ++row;
                    column = 0;
                }
                else if (column >= columns)
                {
                    throw new Exception($"Too many columns in the grid specification (row: {row}).");
                }
                else if ((ch == '.') || (ch == '*'))
                {
                    result[row, column] = (ch == '*');
                    ++column;
                }
                else
                {
                    throw new Exception($"Invalid character {ch} found in grid specification.");
                }
            }

            if (row < rows - 1)
                throw new Exception($"Not enough rows in the grid specification.");
            return result;
        }

        /// <summary>
        /// Read the grid dimensions from a text reader.
        /// </summary>
        /// <param name="reader">Reader object to get the dimensions from.</param>
        /// <param name="rows">Output parameter containing the number of rows.</param>
        /// <param name="columns">Output parameter containing the number of columns.</param>
        private static void ReadDimensions(TextReader reader, out int rows, out int columns)
        {
            // Read dimensions from the first row of the file.
            string dimensionsString = reader.ReadLine();

            if (dimensionsString == null)
            {
                throw new Exception("test.txt is empty.");
            }

            var rowsAndColumnsStrings = dimensionsString.Split(' ');

            if (rowsAndColumnsStrings.Length != 2)
            {
                throw new Exception("test.txt does not contain valid dimensions.");
            }

            int.TryParse(rowsAndColumnsStrings[0], out rows);
            int.TryParse(rowsAndColumnsStrings[1], out columns);
        }

        /// <summary>
        /// Generate a line that separates the rows of a MinesweeperGrid
        /// instance being printed.
        /// </summary>
        /// <param name="minesweeperGrid">Grid to use to determine the length of the separator line.</param>
        /// <returns>A string containing the separator line.</returns>
        private static string GenerateSeparatorLine(MinesweeperGrid minesweeperGrid)
        {
            StringBuilder stringBuilder = new StringBuilder("+");
            for (int column = 0; column < minesweeperGrid.Width; ++column)
            {
                stringBuilder.Append("---+");
            }
            stringBuilder.Append("\n");

            return stringBuilder.ToString();
        }
    }
}
