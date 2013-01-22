using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;

namespace Tests
{
    [TestClass]
    public class SudokuSolverTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SudokuSolver_ExpectsPuzzleInConstructor()
        {
            var solver = new SudokuSolver.SudokuSolver(null);
        }

        [TestMethod]
        public void SudokuSolver_ClonesArgumentToAvoidModifyingOriginal()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.SetValue(1, 4, SudokuValue.NINE);

            var solver = new SudokuSolver.SudokuSolver(puzzle);
            puzzle.ClearBoard();

            Assert.AreEqual(SudokuValue.NINE, solver.Puzzle.GetValue(1, 4));
        }

        [TestMethod]
        public void SudokuSolver_SolvesPuzzle()
        {
            var puzzle = PuzzleMaker.MakeSolvablePuzzle();
            var solver = new SudokuSolver.SudokuSolver(puzzle);

            var solved = solver.Solve();
            Assert.IsTrue(solved);
            Assert.IsTrue(solver.Puzzle.IsSolved());
        }
    }
}
