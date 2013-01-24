from Tkinter import Tk, Frame, Entry, Button, E, S, W, N, CENTER, BOTH
from ui.app_window import *
from model.notifyer import *

class SudokuApp(object):
    def __init__(self):
        notifyer = Notifyer()
        win = Tk()
        AppWindow(win, notifyer)
        win.mainloop()
    
if __name__ == "__main__":
    SudokuApp()
