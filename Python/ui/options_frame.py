from Tkinter import Frame, Button, E, S, W, N, BOTH, TOP

OPTIONS_SOLVE_TEXT = "Solve Puzzle"
OPTIONS_CLEAR_TEXT = "Clear Puzzle"
EVENT_SOLVE_CLICKED = "SolveButtonClicked"
EVENT_CLEAR_CLICKED = "ClearButtonClicked"

class OptionsFrame(Frame):
    def __init__(self, parentWindow, notifyer):
        Frame.__init__(self, parentWindow)
        self.window = parentWindow
        self.notifyer = notifyer
        self.setupUI()

    def setupUI(self):

    	self.rowconfigure(0, pad=10)
    	self.rowconfigure(1, pad=10)
    	self.columnconfigure(0, pad=10)    	

    	clearButton = Button(self, text = OPTIONS_CLEAR_TEXT, command = self.clearClicked)
        clearButton.grid(row = 0, column = 0, pady=10, sticky=N+E+W)

        solveButton = Button(self, text=OPTIONS_SOLVE_TEXT, command = self.solveClicked)
        solveButton.grid(row = 1, column = 0, sticky=S+E+W)        

        self.pack(fill=BOTH)

    def solveClicked(self):
    	self.notifyer.fireEvent(EVENT_SOLVE_CLICKED)

    def clearClicked(self):
    	self.notifyer.fireEvent(EVENT_CLEAR_CLICKED)