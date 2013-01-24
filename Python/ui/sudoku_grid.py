from Tkinter import Frame, Entry, E, S, W, N, BOTH, CENTER, StringVar
from model.sudoku_puzzle import *

NUM_SQUARES = 81

class SudokuGrid(Frame):
    def __init__(self, parentWindow):
        Frame.__init__(self, parentWindow)
        self.window = parentWindow;
        self.setupUI()

    def setupUI(self):
        # create cells
        for i in xrange(9):
            self.rowconfigure(i, pad=3)   
            self.columnconfigure(i, pad=3)                                 

        # validate command for restring input
        vcmd = (self.window.register(self.keyPressHandler), "%d", "%i", "%P", "%s", "%S", "%v", "%V", "%W")

        # add entries
        self.values = []
        for i in xrange(NUM_SQUARES):            
            self.values.append(StringVar())
            tbx = Entry(self, width=3, justify=CENTER, validate="key", validatecommand=vcmd, textvariable=self.values[-1])
            tbx.grid(row = i/9, column = i % 9, sticky=N+E+S+W, padx=2, pady=2)

        self.pack(fill=BOTH)

    # restricts input to numbers, arrows, del and backspace
    def keyPressHandler(self, d, i, P, s, S, v, V, W):
        return S in "123456789"

    def clearBoard(self):
        # clear all entry objects
        for value in self.values:
            value.set('')

        # focus on the first one
        self.winfo_children()[0].focus_set()

    def setPuzzle(self, puzzle):
        for row in xrange(9):
            for col in xrange(9):
                index = row * 9 + col
                if(puzzle.board[row][col] != "0"):
                    self.values[index].set(puzzle.board[row][col])
                else:
                    self.values[index].set('')

    def getPuzzle(self):
        puzzle = SudokuPuzzle()

        for i in xrange(len(self.values)):
            row = i / 9
            col = i % 9
            num = self.values[i].get()
            if num == "" or num == None: num = "0"
            puzzle.setValue(row, col, num)

        return puzzle

    def __str__(self):
        i = 0
        board = ""
        for child in self.values:
            board += '"' + child +'" '
            i += 1
            if i > 0 and i % 9 == 0: board += "\n"

        return board[:-1]