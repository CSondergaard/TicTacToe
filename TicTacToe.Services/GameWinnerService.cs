using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Services
{
    public interface IGameWinnerService
    {
        char Validate(char[,] gameBoard);
        bool CheckIfFree(char[,] gameBoard, int pos1, int pos2);

        char[,] AIPlace(char[,] gameBoard);

        bool CheckIfOExist(char[,] gameBoard);

        bool CheckIfBoardHaveSpace(char[,] gameBoard);

        char[,] CreateBoard();
    }

    public class GameWinnerService : IGameWinnerService
    {

        private const char SymbolForNoWinner = ' ';

        public char Validate(char[,] gameBoard)
        {
            var currentWinningSymbol = CheckForThreeInARowInHorizontalRow(gameBoard);
            if (currentWinningSymbol != SymbolForNoWinner)
                return currentWinningSymbol;
            currentWinningSymbol = CheckForThreeInARowInVerticalColumn(gameBoard);
            if (currentWinningSymbol != SymbolForNoWinner)
                return currentWinningSymbol;
            currentWinningSymbol = CheckForThreeInARowDiagonally(gameBoard);
            if (currentWinningSymbol != SymbolForNoWinner)
                return currentWinningSymbol;

            return SymbolForNoWinner;
        }

        public char[,] CreateBoard()
        {
            char[,] board = new char[3, 3]
                 {
                      {' ', ' ', ' '},
                      {' ', ' ', ' '},
                      {' ', ' ', ' '}
                  };
            return board;
        }

        public char[,] AIPlace(char[,] gameBoard)
        {
            int[] arr = AIPlaceNext(gameBoard);
            int pos1 = arr[0];
            int pos2 = arr[1];
            char[,] ngame = gameBoard;
            ngame[pos1, pos2] = 'O';

            return ngame;
        }

        private int[] AIPlaceNext(char[,] gameBoard)
        {
            int pos1;
            int pos2;
            bool NotFree = true;

            do
            {
                Random rnd = new Random();
                pos1 = rnd.Next(0, 3);
                pos2 = rnd.Next(0, 3);
                if (CheckIfFree(gameBoard, pos1, pos2))
                {
                    NotFree = false;
                }
            }
            while (NotFree);

            int[] pos = { pos1, pos2 };
            return pos;
        }

        public bool CheckIfFree(char[,] gameBoard, int pos1, int pos2)
        {
            var cellchar = gameBoard[pos1, pos2];
            if (cellchar == 'X' || cellchar == 'O')
            {
                return false;
            }
            return true;
        }

        public bool CheckIfBoardHaveSpace(char[,] gameBoard)
        {
            bool free = false;
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (gameBoard[i, k] == ' ')
                    {
                        free = true;
                    }
                }
            }
            return free;
        }

        public bool CheckIfOExist(char[,] gameBoard)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (gameBoard[i, k] == 'O')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static char CheckForThreeInARowInVerticalColumn(char[,] gameBoard)
        {
            for (int i = 0; i < 3; i++)
            {
                    var rowOneChar = gameBoard[0, i];
                    var rowTwoChar = gameBoard[1, i];
                    var rowThreeChar = gameBoard[2, i];
                    if (rowOneChar == rowTwoChar &&
                        rowTwoChar == rowThreeChar)
                    {
                        return rowOneChar;
                    }
                
            }
            return SymbolForNoWinner;
        }

        private static char CheckForThreeInARowInHorizontalRow(char[,] gameBoard)
        {
            for (int i = 0; i < 3; i++)
            {
            
                var columnOneChar = gameBoard[i, 0];
                var columnTwoChar = gameBoard[i, 1];
                var columnThreeChar = gameBoard[i, 2];
                if (columnOneChar == columnTwoChar &&
                    columnTwoChar == columnThreeChar)
                {
                    return columnOneChar;
                }

            }

            return SymbolForNoWinner;
        }

        private static char CheckForThreeInARowDiagonally(char[,] gameBoard)
        {
            var cellOneChar = gameBoard[0, 0];
            var cellTwoChar = gameBoard[1, 1];
            var cellThreeChar = gameBoard[2, 2];
            if (cellOneChar == cellTwoChar &&
                cellTwoChar == cellThreeChar)
            {
                return cellOneChar;
            }
            cellOneChar = gameBoard[0, 2];
            cellTwoChar = gameBoard[1, 1];
            cellThreeChar = gameBoard[2, 0];
            if (cellOneChar == cellTwoChar &&
                cellTwoChar == cellThreeChar)
            {
                return cellOneChar;
            }
            return SymbolForNoWinner;
        }


    }
}
