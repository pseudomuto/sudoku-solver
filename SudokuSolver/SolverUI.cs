using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SolverUI : Form
    {
        private static readonly string CLEAR_LABEL = "Clear";
        private static readonly string CANCEL_LABEL = "Cancel";
        private static readonly string NOT_SOLVEABLE_MESSAGE = "This puzzle is not solveable!";

        private TextBox[] Cells { get; set; }

        private Thread ProcessingThread { get; set; }

        public SolverUI()
        {
            InitializeComponent();

            this.Load += (sender, e) =>
            {
                this.speedSlider.Value = this.speedSlider.Maximum;
                this.Cells = this.GetCellsFromUI();

                foreach (var cell in this.Cells)
                {
                    cell.KeyPress += NumbersOnlyKeypress;
                }
            };
        }

        private void NumbersOnlyKeypress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.KeyChar) || e.KeyChar == (char)8);
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            var puzzle = this.LoadPuzzleState();
            var solver = new SudokuSolver(puzzle);

            int delay = this.speedSlider.Maximum - this.speedSlider.Value;
            this.SetupSolverCallback(solver, delay);

            this.speedSlider.Enabled = false;            
            this.solveButton.Enabled = false;

            this.ProcessingThread = new Thread(new ThreadStart(() =>
            {
                // solve the puzzle!
                if (!solver.Solve())
                {
                    MessageBox.Show(NOT_SOLVEABLE_MESSAGE);
                }

                this.EnableControl(this.speedSlider);                
                this.EnableControl(this.solveButton);

                this.clearButton.Invoke(new Action(() =>
                {
                    this.clearButton.Text = CLEAR_LABEL;
                }));
            }));

            this.clearButton.Text = CANCEL_LABEL;
            this.ProcessingThread.Start();
        }

        private void EnableControl(Control control)
        {
            control.Invoke(new Action(() =>
            {
                control.Enabled = true;
            }));
        }

        private void SetupSolverCallback(SudokuSolver solver, int delay)
        {
            solver.Callback += (solverInstance, args) =>
            {
                // calculate index
                int index = args.Row * 9 + args.Column;
                var value = args.Value == SudokuValue.UNASSIGNED ? string.Empty : ((int)args.Value).ToString();

                this.Cells[index].Invoke(new Action(() =>
                {
                    this.Cells[index].Text = value;
                }));

                args.Delay = delay;
            };
        }

        private SudokuPuzzle LoadPuzzleState()
        {
            var puzzle = new SudokuPuzzle();

            for (int i = 0; i < this.Cells.Length; i++)
            {
                if (!string.IsNullOrEmpty(this.Cells[i].Text))
                {
                    // mark cell as supplied
                    var row = i / puzzle.CurrentBoard.Length;
                    var col = i % puzzle.CurrentBoard.Length;
                    var value = (SudokuValue)Enum.ToObject(typeof(SudokuValue), int.Parse(this.Cells[i].Text));

                    puzzle.SetValue(row, col, value);
                }
            }

            return puzzle;
        }

        private TextBox[] GetCellsFromUI()
        {
            var textboxes = new List<TextBox>();
            foreach (var control in this.Controls)
            {
                if (control is TextBox)
                {
                    textboxes.Add(control as TextBox);
                }
            }

            return textboxes.OrderBy(t => t.TabIndex).ToArray();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (this.clearButton.Text == CLEAR_LABEL)
            {
                foreach (var control in this.Cells)
                {
                    control.Text = string.Empty;
                }
            }
            else
            {
                this.ProcessingThread.Abort();
                this.clearButton.Text = CLEAR_LABEL;
            }
        }
    }
}
