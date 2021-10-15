using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class DisplayBoard
    {
        public static void ChangeWindowSize(DifficultyLevel.DifficultyLevelsEnum difficultyLevel)
        {
            int difficultyLevelNumber = (int)difficultyLevel;

            Console.SetBufferSize(70, 46);

            switch (difficultyLevelNumber)
            {
                case 0:
                    Console.SetWindowSize(34, 28);
                    break;
                case 1:
                    Console.SetWindowSize(42, 32);
                    break;
                case 2:
                    Console.SetWindowSize(70, 46);
                    break;
            }
        }

        public static int DisplayOpenAndNeighbouringCells()
        {
            int openCells = 0;

            DisplayBoardHeader();

            for (int i = 0; i < Board.GameBoard.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < Board.GameBoard.GetLength(1); j++)
                {
                    if (Board.GameBoard[i, j].IsOpen)
                    {
                        DisplayOpenCell(i, j);
                        openCells++;
                    }
                    else DisplayClosedCell();
                }
                DisplayRowOrdinalNumber(i);

                DisplayRowSplitter();
            }
            Console.WriteLine();

            return openCells;
        }

        public static void DisplayGameOverBoard()
        {
            DisplayBoardHeader();

            for (int i = 0; i < Board.GameBoard.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < Board.GameBoard.GetLength(1); j++)
                {
                    if (Board.GameBoard[i, j].IsMine)
                    {
                        DisplayMine();
                    }
                    else if (Board.GameBoard[i, j].IsOpen)
                    {
                        DisplayOpenCell(i, j);
                    }
                    else DisplayClosedCell();
                }
                DisplayRowOrdinalNumber(i);

                DisplayRowSplitter();
            }
            Console.WriteLine();
        }

        public static void DisplayClosedCell()
        {
            Console.Write(" # |");
        }

        public static void DisplayOpenCell(int yAxis, int xAxis)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($" {Board.GameBoard[yAxis, xAxis].MinesAround} ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("|");
        }

        public static void DisplayMine()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" X ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("|");
        }

        public static void DisplayRowOrdinalNumber(int i)
        {
            Console.Write($" {i + 1}");
            Console.WriteLine();
        }

        public static void DisplayBoardHeader()
        {
            DisplayColumnOrdinalNumbers();
            DisplayRowSplitter();
        }

        public static void DisplayColumnOrdinalNumbers()
        {
            Console.Write("|");
            for (int i = 1; i <= Board.GameBoard.GetLength(0); i++)
            {
                if (i.ToString().Length == 1)
                {
                    Console.Write($" {i} |");
                }
                else
                {
                    Console.Write($" {i}|");
                }
            }
            Console.WriteLine();
        }

        public static void DisplayRowSplitter()
        {
            for (int i = 0; i < Board.GameBoard.GetLength(0) + 1; i++)
            {
                Console.Write("|---");
            }
            Console.WriteLine();
        }

        // Development only methods
        public static void DisplayNeighboursNumberTest()
        {
            for (int i = 0; i < Board.GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GameBoard.GetLength(1); j++)
                {
                    Console.Write(Board.GameBoard[i, j].MinesAround + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void DisplayMinesLocationTest()
        {
            for (int i = 0; i < Board.GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GameBoard.GetLength(1); j++)
                {
                    if (Board.GameBoard[i, j].IsMine)
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
    }
}
