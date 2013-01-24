class SudokuPuzzle(object):
    NUM_ROWS = 9
    NUM_CELLS_IN_BOX = 3
    
    def __init__(self):
        self.board = []
        self.clearBoard()

    def __str__(self):
        boardString = ""
        for i in xrange(SudokuPuzzle.NUM_ROWS):
            for j in xrange(SudokuPuzzle.NUM_ROWS):
                boardString += str(self.board[i][j]) + " "

            boardString += "\n"

        return boardString[:-1]
                

    def clearBoard(self):
        self.board = []
        
        for i in xrange(SudokuPuzzle.NUM_ROWS):
            self.board.append([])
            for j in xrange(SudokuPuzzle.NUM_ROWS):
                self.board[i].append('0')

    def setValue(self, row, col, value):
        self.board[row][col] = value

    def getValue(self, row, col):
        return self.board[row][col]

    def isValidValue(self, value, row, col):
        inRow = self.isValueInRow(value, row)
        inCol = self.isValueInCol(value, col)
        inBox = self.isValueInBox(value, row, col)
        
        if inRow or inCol or inBox:
            return False

        return True
    
    def isValueInRow(self, value, row):
        return self.numOccurencesInRow(value, row) > 0

    def isValueInCol(self, value, col):
        return self.numOccurencesInCol(value, col) > 0

    def isValueInBox(self, value, row, col):
        return self.numOccurencesInBox(value, row, col) > 0

    def numOccurencesInRow(self, value, row):
        count = 0
        for i in xrange(SudokuPuzzle.NUM_ROWS):
            if self.board[row][i] == value:
                count += 1

        return count

    def numOccurencesInCol(self, value, col):
        count = 0
        for i in xrange(SudokuPuzzle.NUM_ROWS):
            if self.board[i][col] == value:
                count += 1

        return count

    def numOccurencesInBox(self, value, row, col):
        count = 0

        row = row - row % SudokuPuzzle.NUM_CELLS_IN_BOX
        col = col - col % SudokuPuzzle.NUM_CELLS_IN_BOX

        for i in xrange(row, row + SudokuPuzzle.NUM_CELLS_IN_BOX):
            for j in xrange(col, col + SudokuPuzzle.NUM_CELLS_IN_BOX):
                if self.board[i][j] == value:
                    count += 1

        return count
