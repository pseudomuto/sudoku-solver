from Tkinter import Tk, Frame, Entry, Button, E, S, W, N, CENTER, BOTH

# Constants
NUM_SQUARES = 81

class AppWindow(Frame):
    def __init__(self, parentWindow):
        Frame.__init__(self, parentWindow)
        self.window = parentWindow
        self.setupUI()

    def setupUI(self):
        self.window.title("Sudoku Solver")

        self.rowconfigure(0, pad=10)
        self.rowconfigure(1, pad=10)
        self.columnconfigure(0, pad=10)        
        
        # add the grid
        grid = SudokuGrid(self)
        grid.grid(row = 0, column = 0, sticky=N+E+S+W)

        # add controls
        options = OptionsFrame(self)
        options.grid(row = 1, column = 0, sticky=S+E+W)

        self.pack(fill=BOTH)
        
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

        # add entries
        for i in xrange(NUM_SQUARES):            
            tbx = Entry(self, width=3, justify=CENTER)            
            tbx.grid(row = i/9, column = i % 9, sticky=N+E+S+W, padx=2, pady=2)

        self.pack(fill=BOTH)

class OptionsFrame(Frame):
    def __init__(self, parentWindow):
        Frame.__init__(self, parentWindow)
        self.window = parentWindow
        self.setupUI()

    def setupUI(self):
        button = Button(self, text="Solve Puzzle", pad=3)
        button.pack()
        self.pack(fill=BOTH)
            

def main():
    win = Tk()
    AppWindow(win)
    win.mainloop()
    
if __name__ == "__main__":
    main()
