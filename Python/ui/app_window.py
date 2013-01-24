from Tkinter import Frame, E, S, W, N, BOTH
from model.notifyer import *
from sudoku_grid import *
from options_frame import *

from model.sudoku_puzzle import *
from model.sudoku_solver import *

import threading

class SolverThread(threading.Thread):
    # callback should be a function taking a puzzle (solved) and a bool (success)
    def __init__(self, puzzle, callback):
        threading.Thread.__init__(self)        
        self.puzzle = puzzle
        self.callback = callback

    def run(self):        
        solver = SudokuSolver(self.puzzle, self.notify)
        isSolved = solver.solve()
        self.notify(self.puzzle, isSolved)

    def notify(self, puzzle, solved = False):
        self.callback(puzzle, solved)

class AppWindow(Frame):
    def __init__(self, parentWindow, notifyer):
        Frame.__init__(self, parentWindow)
        self.window = parentWindow
        self.notifyer = notifyer
        self.setupUI()

    def setupUI(self):
        self.window.title("Sudoku Solver")

        self.rowconfigure(0, pad=10)        
        self.columnconfigure(0, pad=10)        
        self.columnconfigure(1, pad=10)

        # setup listeners
        self.notifyer.addListener(EVENT_SOLVE_CLICKED, self.solveGrid)
        self.notifyer.addListener(EVENT_CLEAR_CLICKED, self.clearGrid)
        
        # add the grid
        self.board = SudokuGrid(self)
        self.board.grid(row = 0, column = 0, sticky=N+E+S+W)

        # add controls
        self.options = OptionsFrame(self, self.notifyer)
        self.options.grid(row = 0, column = 1, sticky=N+S+E+W)

        self.pack(fill=BOTH)

    def solverCallback(self, puzzle, isSolved):
        print "Handling callback"
        print puzzle
        self.board.setPuzzle(puzzle)        

    def clearGrid(self, data = None):        
        self.board.clearBoard()

    def solveGrid(self, data = None):
        puzzle = self.board.getPuzzle()
        print puzzle

        thread = SolverThread(puzzle, self.solverCallback)
        thread.start()