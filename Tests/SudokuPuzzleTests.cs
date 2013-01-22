using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;

namespace Tests
{
    [TestClass]
    public class SudokuPuzzleTests
    {
        [TestMethod]
        public void SudokuPuzzle_AllCellsDefaultToSudokuValueUnassigned()
        {
            var puzzle = new SudokuPuzzle();

            for (int i = 0; i < puzzle.CurrentBoard.Length; i++)
            {
                // board is a square...
                for (int j = 0; j < puzzle.CurrentBoard.Length; j++)
                {
                    Assert.AreEqual(SudokuValue.UNASSIGNED, puzzle.CurrentBoard[i][j]);
                }
            }
        }

        [TestMethod]
        public void SudokuPuzzle_CanSetValueForCell()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.SetValue(0, 3, SudokuValue.NINE);

            Assert.AreEqual(SudokuValue.NINE, puzzle.GetValue(0, 3));
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SudokuPuzzle_SettingValueOutOfBoundsThrowsException()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.SetValue(0, 9, SudokuValue.NINE);
        }

        
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SudokuPuzzle_GettingValueOutOfBoundsThrowsException()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.GetValue(9, 0);
        }

        [TestMethod]
        public void SudokuPuzzle_CanClearBoard()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.SetValue(0, 3, SudokuValue.NINE);
            puzzle.SetValue(0, 4, SudokuValue.EIGHT);

            puzzle.ClearBoard();

            for (int i = 0; i < puzzle.CurrentBoard.Length; i++)
            {
                // board is a square...
                for (int j = 0; j < puzzle.CurrentBoard.Length; j++)
                {
                    Assert.AreEqual(SudokuValue.UNASSIGNED, puzzle.CurrentBoard[i][j]);
                }
            }
        }

        [TestMethod]
        public void SudokuPuzzle_ValidatesSameValueInRow()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.SetValue(0, 3, SudokuValue.NINE);

            Assert.IsFalse(puzzle.IsValidValue(SudokuValue.NINE, 0, 1));
        }

        [TestMethod]
        public void SudokuPuzzle_ValidatesSameValueInColumn()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.SetValue(0, 3, SudokuValue.NINE);

            Assert.IsFalse(puzzle.IsValidValue(SudokuValue.NINE, 1, 3));
        }

        [TestMethod]
        public void SudokuPuzzle_ValidatesSameValueInBox()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.SetValue(1, 4, SudokuValue.NINE);

            Assert.IsFalse(puzzle.IsValidValue(SudokuValue.NINE, 2, 3));
        }

        [TestMethod]
        public void SudokuPuzzle_CanCloneItself()
        {
            var puzzle = new SudokuPuzzle();
            puzzle.SetValue(1, 4, SudokuValue.NINE);
            puzzle.SetValue(2, 3, SudokuValue.FIVE);

            var clone = puzzle.Clone() as SudokuPuzzle;
            puzzle.ClearBoard();

            Assert.AreEqual(SudokuValue.NINE, clone.GetValue(1, 4));
            Assert.AreEqual(SudokuValue.FIVE, clone.GetValue(2, 3));
        }

        [TestMethod]
        public void SudokuPuzzle_KnowsIfItIsSolved()
        {
            var puzzle = PuzzleMaker.MakeSolvedPuzzle();
            Assert.IsTrue(puzzle.IsSolved());
        }

        [TestMethod]
        public void SudokuPuzzle_KnowsIfItIsNotSolved()
        {
            var puzzle = PuzzleMaker.MakeSolvedPuzzle();
            puzzle.SetValue(0, 0, SudokuValue.FIVE);
            Assert.IsFalse(puzzle.IsSolved());
        }
    }
}
