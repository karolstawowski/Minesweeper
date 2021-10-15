using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class Board
    {
        static readonly int[] boardWidths = new int[]
        {
            7, 9, 16
        };
        static readonly int[] boardHeights = new int[]
        {
            7, 9, 16
        };
        static readonly int[] numberOfMines = new int[]
        {
            8, 10, 40
        };
        static readonly int[] maxOpenCellsNumber = new int[]
        {
            41, 71, 216
        };

        public static int BoardWidth;

        public static int BoardHeight;

        public static Cell[,] GameBoard;

        static int _boardMinesNumber;

        public static void SetBoardSize()
        {
            int selectedGameDifficulty = Convert.ToInt32(Game.gameDifficulty);

            BoardWidth = boardWidths[selectedGameDifficulty];
            BoardHeight = boardHeights[selectedGameDifficulty];
            _boardMinesNumber = numberOfMines[selectedGameDifficulty];
            GameBoard = new Cell[BoardHeight, BoardWidth];
        }

        public static void InitializeBoard()
        {
            for (int i = 0; i < BoardHeight; i++)
            {
                for (int j = 0; j < BoardWidth; j++)
                {
                    Board.GameBoard[i, j] = new Cell(false, i, j, false, 0);
                }
            }
        }

        public static int GetMinesNumber(int difficultyLevel)
        {
            return numberOfMines[difficultyLevel];
        }

        public static int GetMaxOpenCellsNumber(int difficultyLevel)
        {
            return maxOpenCellsNumber[difficultyLevel];
        }

        public static void AssingMines()
        {
            MineRandomiser.AddMines(ref Board.GameBoard, _boardMinesNumber);
        }
    }
}
