# import the puzzle class
from sudoku_puzzle import *

class SudokuSolver(object):
    def __init__(self, puzzle):
        self.puzzle = puzzle

    def solve(self):
        row, col = 0, 0
        cell = self.getNextCell()

        # solved!
        if cell == None: return True
        
        row = cell[0]
        col = cell[1]
        choices = self.getChoices(row, col)

        for option in choices:
            self.puzzle.setValue(row, col, option)
            if self.solve(): return True
            self.puzzle.setValue(row, col, '0')

    def getChoices(self, row, col):
        choices = []

        for option in xrange(1, 10):
            if self.puzzle.isValidValue(option, row, col):
                choices.append(option)

        return choices

    def getNextCell(self):
        for i in xrange(9):
            for j in xrange(9):
                if self.puzzle.board[i][j] == '0':
                    return (i, j)

        return None
