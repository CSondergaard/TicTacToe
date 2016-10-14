using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToe.UnitTests
{
    [TestClass]
    public class GameWinnerServiceTests
    {
        private IGameWinnerService _gameWinnerService;
        private char[,] _gameBoard;

        [TestInitialize]
        public void SetupUnitTests()
        {
            _gameWinnerService = new GameWinnerService();
            _gameBoard = new char[3, 3]
                  {
                      {' ', ' ', ' '},
                      {' ', ' ', ' '},
                      {' ', ' ', ' '}
                  };
        }
        [TestMethod]
        public void NeitherPlayerHasThreeInARow()
        {
            const char expected = ' ';
            var actual = _gameWinnerService.Validate(_gameBoard);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void oonotwinning()
        {
            const char expected = ' ';
            _gameBoard[0, 0] = 'X';
            var actual = _gameWinnerService.Validate(_gameBoard);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void PlayerWithAllSpacesInTopRowIsWinner()
        {
            const char expected = 'X';
            for (var rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                _gameBoard[0, rowIndex] = expected;
            }
            var actual = _gameWinnerService.Validate(_gameBoard);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void PlayerWithAllSpacesInFirstColumnIsWinner()
        {
            const char expected = 'X';
            for (var columnIndex = 0; columnIndex < 3; columnIndex++)
            {
                _gameBoard[columnIndex, 0] = expected;
            }
            var actual = _gameWinnerService.Validate(_gameBoard);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void PlayerWithThreeInARowDiagonallyDownAndToRightIsWinner()
        {
            const char expected = 'X';
            for (var cellIndex = 0; cellIndex < 3; cellIndex++)
            {
                _gameBoard[cellIndex, cellIndex] = expected;
            }
            var actual = _gameWinnerService.Validate(_gameBoard);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void PlayerWithThreeInARowDiagonallyUpAndToRightIsWinner()
        {
            const char expected = 'X';
            _gameBoard[0, 2] = expected;
            _gameBoard[1, 1] = expected;
            _gameBoard[2, 0] = expected;

            var actual = _gameWinnerService.Validate(_gameBoard);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void CharExistingOnField()
        {
            const char expected = 'X';
            _gameBoard[0, 1] = expected;
            _gameBoard[0, 2] = 'O';
            bool actual = _gameWinnerService.CheckIfFree(_gameBoard, 0, 1);
            bool actual2 = _gameWinnerService.CheckIfFree(_gameBoard, 0, 2);
            bool free = _gameWinnerService.CheckIfFree(_gameBoard, 0, 0);
            Assert.IsFalse(actual);
            Assert.IsFalse(actual2);
            Assert.IsTrue(free);
        }

        [TestMethod]
        public void AiPlacing()
        {
            _gameBoard[0, 0] = 'X';
            _gameBoard = _gameWinnerService.AIPlace(_gameBoard);
            Assert.IsTrue(_gameWinnerService.CheckIfOExist(_gameBoard));


        }

        [TestMethod]
        public void BoardFull()
        {
            Assert.IsTrue(_gameWinnerService.CheckIfBoardHaveSpace(_gameBoard));
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    _gameBoard[i, k] = 'X';
                }
            }
            Assert.IsFalse(_gameWinnerService.CheckIfBoardHaveSpace(_gameBoard));


           
        }
    }
}
