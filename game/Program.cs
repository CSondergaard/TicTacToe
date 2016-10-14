using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Services;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {

            consolegame game = new consolegame();
            game.Play();
        }
    }
}
