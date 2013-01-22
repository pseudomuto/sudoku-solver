using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class PuzzleMaker
    {
        public static SudokuPuzzle MakeSolvedPuzzle()
        {
            // known solution from: http://www.sudokukingdom.com/images/rules_solved.jpg
            var rows = new SudokuValue[][] {
                new SudokuValue [] { SudokuValue.TWO, SudokuValue.FOUR, SudokuValue.EIGHT, SudokuValue.THREE, SudokuValue.NINE, SudokuValue.FIVE, SudokuValue.SEVEN, SudokuValue.ONE, SudokuValue.SIX },
                new SudokuValue [] { SudokuValue.FIVE, SudokuValue.SEVEN, SudokuValue.ONE, SudokuValue.SIX, SudokuValue.TWO, SudokuValue.EIGHT, SudokuValue.THREE, SudokuValue.FOUR, SudokuValue.NINE },
                new SudokuValue [] { SudokuValue.NINE, SudokuValue.THREE, SudokuValue.SIX, SudokuValue.SEVEN, SudokuValue.FOUR, SudokuValue.ONE, SudokuValue.FIVE, SudokuValue.EIGHT, SudokuValue.TWO },
                
                new SudokuValue [] { SudokuValue.SIX, SudokuValue.EIGHT, SudokuValue.TWO, SudokuValue.FIVE, SudokuValue.THREE, SudokuValue.NINE, SudokuValue.ONE, SudokuValue.SEVEN, SudokuValue.FOUR },
                new SudokuValue [] { SudokuValue.THREE, SudokuValue.FIVE, SudokuValue.NINE, SudokuValue.ONE, SudokuValue.SEVEN, SudokuValue.FOUR, SudokuValue.SIX, SudokuValue.TWO, SudokuValue.EIGHT },
                new SudokuValue [] { SudokuValue.SEVEN, SudokuValue.ONE, SudokuValue.FOUR, SudokuValue.EIGHT, SudokuValue.SIX, SudokuValue.TWO, SudokuValue.NINE, SudokuValue.FIVE, SudokuValue.THREE },

                new SudokuValue [] { SudokuValue.EIGHT, SudokuValue.SIX, SudokuValue.THREE, SudokuValue.FOUR, SudokuValue.ONE, SudokuValue.SEVEN, SudokuValue.TWO, SudokuValue.NINE, SudokuValue.FIVE },
                new SudokuValue [] { SudokuValue.ONE, SudokuValue.NINE, SudokuValue.FIVE, SudokuValue.TWO, SudokuValue.EIGHT, SudokuValue.SIX, SudokuValue.FOUR, SudokuValue.THREE, SudokuValue.SEVEN },
                new SudokuValue [] { SudokuValue.FOUR, SudokuValue.TWO, SudokuValue.SEVEN, SudokuValue.NINE, SudokuValue.FIVE, SudokuValue.THREE, SudokuValue.EIGHT, SudokuValue.SIX, SudokuValue.ONE }
            };

            var puzzle = new SudokuPuzzle();
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    puzzle.SetValue(i, j, rows[i][j]);
                }
            }

            return puzzle;
        }

        public static SudokuPuzzle MakeSolvablePuzzle()
        {
            var puzzle = new SudokuPuzzle();

            // set some values
            puzzle.SetValue(0, 0, SudokuValue.EIGHT);
            puzzle.SetValue(0, 2, SudokuValue.FOUR);
            puzzle.SetValue(0, 7, SudokuValue.FIVE);
            puzzle.SetValue(1, 3, SudokuValue.FOUR);
            puzzle.SetValue(1, 6, SudokuValue.SEVEN);
            puzzle.SetValue(1, 8, SudokuValue.SIX);
            
            puzzle.SetValue(2, 1, SudokuValue.SEVEN);
            puzzle.SetValue(2, 3, SudokuValue.TWO);
            puzzle.SetValue(2, 4, SudokuValue.FIVE);
            puzzle.SetValue(2, 5, SudokuValue.NINE);

            puzzle.SetValue(8, 1, SudokuValue.FOUR);
            puzzle.SetValue(8, 5, SudokuValue.FIVE);
            puzzle.SetValue(8, 8, SudokuValue.SEVEN);

            return puzzle;
        }
    }
}
