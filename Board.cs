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
        public static readonly int[] minesNumbers = new int[]
        {
            8, 10, 40
        };
        public static readonly int[] maxOpenCells = new int[]
        {
            41, 71, 216
        };

        public static int boardWidth;
        public static int boardHeight;

        public static Cell[,] gameBoard;

        public static int boardMinesNumber;

        public static int boardOpenCells;
        public static void SetBoardSize()
        {
            int selectedGameDifficulty = Convert.ToInt32(Game.gameDifficulty);

            boardWidth = boardWidths[selectedGameDifficulty];
            boardHeight = boardHeights[selectedGameDifficulty];
            boardMinesNumber = minesNumbers[selectedGameDifficulty];
            gameBoard = new Cell[boardHeight, boardWidth];
        }

        public static void InitializeBoard()
        {
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    Board.gameBoard[i, j] = new Cell(false, i, j, false, 0);
                }
            }
        }

        public static void AssingMines()
        {
            MineRandomiser.AddMines(ref Board.gameBoard, boardMinesNumber);
        }

        public static void FirstBoardDisplay()
        {
            for (int i = 0; i < Board.gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < Board.gameBoard.GetLength(1); j++)
                {
                    if (Board.gameBoard[i, j].isOpen)
                    {
                        Console.Write(Board.gameBoard[i, j].mineNeighbours);
                    }
                    else Console.Write("# ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void DisplayMineLocationsTest()
        {
            for (int i = 0; i < Board.gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < Board.gameBoard.GetLength(1); j++)
                {
                    if (Board.gameBoard[i, j].isMine)
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write("O ");
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void DisplayColumnNumbers()
        {
            Console.Write("|");
            for (int k = 1; k <= Board.gameBoard.GetLength(0); k++)
            {
                if (k.ToString().Length == 1)
                {
                    Console.Write($" {k} |");
                }
                else
                {
                    Console.Write($" {k}|");
                }
            }
            Console.WriteLine();
        }

        public static void DisplayBoardHeader()
        {
            DisplayColumnNumbers();
            DisplayRowSplitter();
        }

        public static void DisplayRowSplitter()
        {
            for (int k = 0; k < Board.gameBoard.GetLength(0) + 1; k++)
            {
                Console.Write("|---");
            }
            Console.WriteLine();
        }

        public static void DisplayRowNumber(int i)
        {
            Console.Write($" {i + 1}");
            Console.WriteLine();
        }

        public static void DisplayGameOverBoard()
        {
            Board.DisplayBoardHeader();

            for (int i = 0; i < Board.gameBoard.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < Board.gameBoard.GetLength(1); j++)
                {
                    if (Board.gameBoard[i, j].isMine)
                    {
                        Cell.DisplayMine();
                    }
                    else if (Board.gameBoard[i, j].isOpen)
                    {
                        Cell.DisplayOpenCell(i, j);
                    }
                    else Cell.DisplayClosedCell(i, j);
                }
                Board.DisplayRowNumber(i);

                Board.DisplayRowSplitter();
            }
            Console.WriteLine();
        }

        public static int DisplayOpenAndNeighbouringCells()
        {
            int openCells = 0;

            Board.DisplayBoardHeader();

            for (int i = 0; i < Board.gameBoard.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < Board.gameBoard.GetLength(1); j++)
                {
                    if (Board.gameBoard[i, j].isOpen)
                    {
                        Cell.DisplayOpenCell(i, j);
                        openCells++;
                    }
                    else Cell.DisplayClosedCell(i, j);
                }
                Board.DisplayRowNumber(i);

                Board.DisplayRowSplitter();
            }
            Console.WriteLine();

            return openCells;
        }
    }
}
