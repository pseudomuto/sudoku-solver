# import the puzzle class
from sudoku_puzzle import *

class SudokuSolver(object):
    """A class that implements a recursive backtracking algorithm to solve a SudokuPuzzle"""

    def __init__(self, puzzle, callback = None):
        """Initializes a solver

        puzzle: The puzzle to solve
        callback: An optional callback method that will be called whenever a cell's value changes
        """
        self.puzzle = puzzle
        self.callback = callback

    def solve(self):
        """Attempts to solve the puzzle and returns whether or not it was successful"""
        row, col = 0, 0
        cell = self.getNextCell()

        # solved!
        if cell == None: return True
        
        row = cell[0]
        col = cell[1]
        choices = self.getChoices(row, col)

        for option in choices:
            self.puzzle.setValue(row, col, option)
            self.notify()
            
            if self.solve(): return True

            self.puzzle.setValue(row, col, '0')
            self.notify()

        return False

    def notify(self):
        """Notified callback if it was supplied in the constructor"""
        if self.callback != None:
            self.callback(self.puzzle)

    def getChoices(self, row, col):
        """Determines which values are valid for the specified cell and returns a list"""
        choices = []

        for option in xrange(1, 10):
            if self.puzzle.isValidValue(option, row, col):
                choices.append(option)

        return choices

    def getNextCell(self):
        """Finds the next empty cell and returns a tuple (x, y). If not found None will be returned"""
        for i in xrange(9):
            for j in xrange(9):
                if self.puzzle.board[i][j] == '0':
                    return (i, j)

        return None
