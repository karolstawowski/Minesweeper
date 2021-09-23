using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class MineRandomiser
    {
        public static void AddMines(ref Cell[,] board, int requiredNumberOfMines)
        {
            int numberOfGeneratedMines = 0;
            Random random = new Random();
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);

            while(numberOfGeneratedMines < requiredNumberOfMines)
            {
                int X = random.Next(0, boardWidth);
                int Y = random.Next(0, boardHeight);

                if (board[Y, X].isMine == false)
                {
                    board[Y, X].isMine = true;
                    numberOfGeneratedMines++;
                }
            }
        }
    }
}
