using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public enum SudokuSolverEventArgsAction
    {
        Set,
        Unset
    }

    public class SudokuSolverEventArgs : EventArgs
    {
        public int Row { get; private set; }
        
        public int Column { get; private set; }

        public SudokuValue Value { get; private set; }

        public SudokuSolverEventArgsAction Action { get; private set; }

        public int Delay { get; set; }

        public SudokuSolverEventArgs(SudokuValue value, int row, int column, SudokuSolverEventArgsAction action = SudokuSolverEventArgsAction.Set)
            : base()
        {
            this.Row = row;
            this.Column = column;
            this.Value = value;
            this.Action = action;
        }
    }
}
