using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class Alert
    {
        public static void GameWonAlert()
        {
            Console.WriteLine("You won the game!");
            Console.WriteLine();
        }
        public static void GameOverAlert()
        {
            Console.WriteLine("You lost the game.");
            Console.WriteLine();
        }
        public static void GoodbyeAlert()
        {
            Console.Clear();
            Console.SetWindowSize(47, 18);
            Console.SetBufferSize(47, 18);
            Console.WriteLine("Thanks for playing the game!");
            Console.WriteLine("Minesweeper implementation by Karol Stawowski");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
