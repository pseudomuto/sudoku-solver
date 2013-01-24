class SudokuPuzzle(object):
    """A class that represents a Sudoku puzzle"""

    NUM_ROWS = 9
    NUM_CELLS_IN_BOX = 3
    
    def __init__(self):
        self.board = []
        self.clearBoard()

    def __str__(self):
        """Generates a grid value all the values of the puzzle"""
        boardString = ""
        for i in xrange(SudokuPuzzle.NUM_ROWS):
            for j in xrange(SudokuPuzzle.NUM_ROWS):
                boardString += str(self.board[i][j]) + " "

            boardString += "\n"

        return boardString[:-1]
                

    def clearBoard(self):
        """Resets all cells back to empty"""
        self.board = []
        
        for i in xrange(SudokuPuzzle.NUM_ROWS):
            self.board.append([])
            for j in xrange(SudokuPuzzle.NUM_ROWS):
                self.board[i].append('0')

    def setValue(self, row, col, value):
        """Sets the value for the specified cell"""
        self.board[row][col] = value

    def getValue(self, row, col):
        """Returns the value for the specified cell"""
        return self.board[row][col]

    def isValidValue(self, value, row, col):
        """Determines whether the specified value is valid for the cell

        This method will verify that the value does not occur in the row, cell or 
        box containing (row, col)
        """
        inRow = self.isValueInRow(value, row)
        inCol = self.isValueInCol(value, col)
        inBox = self.isValueInBox(value, row, col)
        
        if inRow or inCol or inBox:
            return False

        return True
    
    def isValueInRow(self, value, row):
        """Checks to see if the value is present in the specified row"""
        return self.numOccurencesInRow(value, row) > 0

    def isValueInCol(self, value, col):
        """Checks to see if the value is present in the specified column"""
        return self.numOccurencesInCol(value, col) > 0

    def isValueInBox(self, value, row, col):
        """Checks to see if the value is present in the specified box (3x3 containing row and col)"""
        return self.numOccurencesInBox(value, row, col) > 0

    def numOccurencesInRow(self, value, row):
        """Counts the number of times value appears in the specified row and returns the number"""
        count = 0
        for i in xrange(SudokuPuzzle.NUM_ROWS):
            if self.board[row][i] == value:
                count += 1

        return count

    def numOccurencesInCol(self, value, col):
        """Counts the number of times value appears in the specified column and returns the number"""
        count = 0
        for i in xrange(SudokuPuzzle.NUM_ROWS):
            if self.board[i][col] == value:
                count += 1

        return count

    def numOccurencesInBox(self, value, row, col):
        """Counts the number of times value appears in the box containing (row, col)"""
        count = 0

        row = row - row % SudokuPuzzle.NUM_CELLS_IN_BOX
        col = col - col % SudokuPuzzle.NUM_CELLS_IN_BOX

        for i in xrange(row, row + SudokuPuzzle.NUM_CELLS_IN_BOX):
            for j in xrange(col, col + SudokuPuzzle.NUM_CELLS_IN_BOX):
                if self.board[i][j] == value:
                    count += 1

        return count
