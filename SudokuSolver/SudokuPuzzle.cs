using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public enum SudokuValue
    {
        UNASSIGNED = 0,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE
    }

    public class SudokuPuzzle : ICloneable
    {
        private static readonly int NUM_ROWS = 9;
        private static readonly int NUM_CELLS_PER_BOX = (int)Math.Sqrt(NUM_ROWS);

        public SudokuValue[][] CurrentBoard { get; private set; }

        public SudokuPuzzle()
        {
            // initialize board
            this.ClearBoard();
        }

        public void ClearBoard()
        {
            this.CurrentBoard = new SudokuValue[NUM_ROWS][];
            for (int i = 0; i < NUM_ROWS; i++)
            {
                this.CurrentBoard[i] = new SudokuValue[NUM_ROWS];
            }
        }

        public void SetValue(int row, int col, SudokuValue value)
        {
            this.CurrentBoard[row][col] = value;
        }

        public SudokuValue GetValue(int row, int col)
        {
            return this.CurrentBoard[row][col];
        }

        public bool IsValidValue(SudokuValue sudokuValue, int row, int col)
        {
            if (this.IsValueInRow(sudokuValue, row) || this.IsValueInColumn(sudokuValue, col) || this.IsValueInBox(sudokuValue, row, col))
            {
                return false;
            }

            return true;
        }

        public bool IsSolved()
        {
            for (int i = 0; i < NUM_ROWS; i++)
            {
                for (int j = 0; j < NUM_ROWS; j++)
                {
                    var value = this.GetValue(i, j);
                    var rowCount = this.CountOccurencesInRow(value, i);
                    var colCount = this.CountOccurencesInColumn(value, j);
                    var boxCount = this.CountOccurencesInBox(value, i, j);

                    // single occurence max!
                    if (rowCount > 1 || colCount > 1 || boxCount > 1) return false;
                }
            }

            return true;
        }

        #region  [ICloneable Implementation]

        public object Clone()
        {
            var puzzle = new SudokuPuzzle();

            for (int i = 0; i < NUM_ROWS; i++)
            {
                for (int j = 0; j < NUM_ROWS; j++)
                {
                    puzzle.SetValue(i, j, this.GetValue(i, j));
                }
            }

            return puzzle;
        } 

        #endregion

        #region [Private Helper Methods]

        private bool IsValueInRow(SudokuValue value, int row)
        {
            return this.CountOccurencesInRow(value, row) > 0;
        }

        private bool IsValueInColumn(SudokuValue value, int col)
        {
            return this.CountOccurencesInColumn(value, col) > 0;
        }

        private bool IsValueInBox(SudokuValue value, int row, int col)
        {
            return this.CountOccurencesInBox(value, row, col) > 0;
        }

        private int CountOccurencesInRow(SudokuValue value, int row)
        {
            var count = 0;

            for (int i = 0; i < NUM_ROWS; i++)
            {
                if (this.CurrentBoard[row][i] == value)
                {
                    count++;
                }
            }

            return count;
        }

        private int CountOccurencesInColumn(SudokuValue value, int col)
        {
            var count = 0;

            for (int i = 0; i < NUM_ROWS; i++)
            {
                if (this.CurrentBoard[i][col] == value)
                {
                    count++;
                }
            }

            return count;
        }

        private int CountOccurencesInBox(SudokuValue value, int row, int col)
        {
            var count = 0;

            // adjust (0, 1, 2) => 0, (3, 4, 5) => 3, (6, 7, 8) => 6
            row = row - row % NUM_CELLS_PER_BOX;
            col = col - col % NUM_CELLS_PER_BOX;

            for (int i = row; i < row + NUM_CELLS_PER_BOX; i++)
            {
                for (int j = col; j < col + NUM_CELLS_PER_BOX; j++)
                {
                    if (this.CurrentBoard[i][j] == value)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        #endregion               
    }
}
