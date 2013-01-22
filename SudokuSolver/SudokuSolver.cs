using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SudokuSolver
    {
        public SudokuPuzzle Puzzle { get; private set; }

        public EventHandler<SudokuSolverEventArgs> Callback { get; set; }

        public SudokuSolver(SudokuPuzzle puzzle)
        {
            if (puzzle == null)
            {
                throw new ArgumentNullException("puzzle");
            }
                        
            this.Puzzle = puzzle.Clone() as SudokuPuzzle;
        }

        public bool Solve()
        {
            var row = 0;
            var col = 0;

            // solved!
            if (!this.IsNextCellAvailable(out row, out col)) return true;

            var choices = this.GetValidChoicesForCell(row, col);

            for (int i = 0; i < choices.Count(); i++)
            {
                this.Puzzle.SetValue(row, col, choices[i]);
                this.NotifyCallback(choices[i], row, col, SudokuSolverEventArgsAction.Set);                

                if (this.Solve()) return true;

                this.Puzzle.SetValue(row, col, SudokuValue.UNASSIGNED);
                this.NotifyCallback(choices[i], row, col, SudokuSolverEventArgsAction.Unset);
            }

            return false;
        }

        private SudokuValue[] GetValidChoicesForCell(int row, int col)
        {
            var choices = new List<SudokuValue>();
            var values = Enum.GetNames(typeof(SudokuValue));

            foreach (var value in values)
            {
                var enumObject = (SudokuValue)Enum.Parse(typeof(SudokuValue), value);
                if (enumObject != SudokuValue.UNASSIGNED)
                {
                    if (this.Puzzle.IsValidValue(enumObject, row, col))
                    {
                        choices.Add(enumObject);
                    }
                }
            }

            return choices.ToArray();
        }

        private bool IsNextCellAvailable(out int row, out int col)
        {
            // output params require assignment
            // setting to -1 to cause IndexOutOfRangeException if used when false is returned
            row = -1;
            col = -1;

            for (int i = 0; i < this.Puzzle.CurrentBoard.Length; i++)
            {
                for (int j = 0; j < this.Puzzle.CurrentBoard.Length; j++)
                {
                    if (this.Puzzle.CurrentBoard[i][j] == SudokuValue.UNASSIGNED)
                    {
                        row = i;
                        col = j;
                        return true;
                    }
                }
            }

            return false;
        }

        private void NotifyCallback(SudokuValue value, int row, int col, SudokuSolverEventArgsAction action)
        {
            if (this.Callback != null)
            {
                var args = new SudokuSolverEventArgs(value, row, col, action);
                this.Callback.Invoke(this, args);

                if (args.Delay > 0)
                {
                    Thread.Sleep(args.Delay);
                }
            }
        }
    }
}