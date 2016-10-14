using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Services;

namespace TicTacToe.Services
{
    public class consolegame
    {

        private char[,] board = new char[3, 3]
               {
                      {' ', ' ', ' '},
                      {' ', ' ', ' '},
                      {' ', ' ', ' '}
                  };
        string pos1, pos2, player;
        char winnerCheck, choice;
        int p1r, p2r;
        bool NoWinner, running;
        bool SpaceFilled = false;
        private IGameWinnerService _game;

        public void Print()
        {
            Console.WriteLine("   ----0-------1-------2----\n" +
                             "   |       |       |       |\n" +
                             " 0 |   {0}   |   {1}   |   {2}   |\n" +
                             "   |_______|_______|_______|\n" +
                             "   |       |       |       |\n" +
                             " 1 |   {3}   |   {4}   |   {5}   |\n" +
                             "   |_______|_______|_______|\n" +
                             "   |       |       |       |\n" +
                             " 2 |   {6}   |   {7}   |   {8}   |\n" +
                             "   |_______|_______|_______|\n",
                             board[0, 0], board[0, 1], board[0, 2],
                             board[1, 0], board[1, 1], board[1, 2],
                             board[2, 0], board[2, 1], board[2, 2]);
        }

        public void Play()
        {
            Console.WriteLine("Indtast spiller navn");
            player = Console.ReadLine();
            do
            {
                Print();
                do
                {
                    _game = new GameWinnerService();
                    // User Turn

                    do
                    {
                        Console.WriteLine("Det er " + player + "s tur");
                        Console.Write(player + " indsæt lodret række ");
                        pos1 = Console.ReadLine();

                        do
                        {
                            if (int.TryParse(pos1, out p1r))
                            {
                                while (p1r < 0 || p1r > 2)
                                {
                                    Console.WriteLine("Du skal indtaste et tal mellem (0 - 2) for din lodrette række");
                                    pos1 = Console.ReadLine();
                                    if (int.TryParse(pos1, out p1r))
                                    {

                                    }
                                }
                            }
                            else
                            {
                                while (!int.TryParse(pos1, out p1r))
                                {
                                    Console.WriteLine("Du skal indtaste et tal for din lodrette række");
                                    pos1 = Console.ReadLine();
                                }
                            }
                        }
                        while (!int.TryParse(pos1, out p1r) && p1r < 0 || p1r > 2);




                        Console.Write(player + " indsæt vandret række:  ");
                        pos2 = Console.ReadLine();

                        while (!int.TryParse(pos2, out p2r))
                        {
                            Console.WriteLine("Du skal indtaste et tal for din vandret række");
                            pos2 = Console.ReadLine();
                        }
                        while (p2r < 0 || p2r > 2)
                        {
                            Console.WriteLine("Du skal indtaste et tal mellem (0 - 2) for din vandret række");
                            pos2 = Console.ReadLine();
                        }
                        if (_game.CheckIfFree(board, p1r, p2r))
                        {
                            board[p1r, p2r] = 'X';
                            SpaceFilled = false;
                        }
                        else
                        {
                            Console.WriteLine("Indtastet plads er allerede taget");
                        }

                    }
                    while (SpaceFilled);
                    SpaceFilled = true;
                    //Check for winner

                    NoWinner = CheckForWinner();
                    if (NoWinner)
                    {
                        //AI placement
                        if (board[1,1] == 'X' || board[1,1] == 'O')
                        {
                            AIPlace();
                        }
                        else
                        {
                            board[1, 1] = 'O';
                        }
                        
                        

                        // Check for winner
                        NoWinner = CheckForWinner();
                        if (NoWinner)
                        {
                            Console.Clear();
                            Print();

                        }
                    }



                }
                while (NoWinner);

                Console.WriteLine("Spille igen? [Y] [N]");
                choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case 'y':
                        NoWinner = true;
                        running = true;
                        Console.Clear();
                        board = _game.CreateBoard();
                        break;
                    case 'n':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Kan ikke genkende dit svar");
                        break;

                }
            }
            while (running);
        }
        public void AIPlace()
        {
            if (_game.CheckIfBoardHaveSpace(board))
            {
                _game.AIPlace(board);
            }
            else
            {
                board = _game.CreateBoard();
            }
        }

        public bool CheckForWinner()
        {
            winnerCheck = _game.Validate(board);
            if (winnerCheck == 'X')
            {
                Console.Clear();
                Print();
                Console.WriteLine("Tillykke " + player + " du vandt spillet");
                return false;
            }
            else if (winnerCheck == 'O')
            {
                Console.Clear();
                Print();
                Console.WriteLine("Du tabte til computeren");
                return false;
            }
            return true;

        }

    }
}
