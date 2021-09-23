using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Cell
    {
        static readonly int[] neighboursLocations = new int[]
        {
            -1,0,1
        };

        public bool isOpen = false;
        int row = -1;
        int column = -1;
        public bool isMine;
        public int mineNeighbours;
        public Cell(bool isOpen, int row, int column, bool isMine, int mineNeighbours)
        {
            this.isOpen = isOpen;
            this.row = row;
            this.column = column;
            this.isMine = isMine;
            this.mineNeighbours = mineNeighbours;
        }
        public static void getNeighbours(Cell[,] array)
        {
            int neighboursCount = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    foreach (int row in neighboursLocations)
                    {
                        foreach (int col in neighboursLocations)
                        {
                            try
                            {
                                if (array[i + row, j + col].isMine)
                                {
                                    if (row != 0 || col != 0)
                                    {
                                        neighboursCount++;
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    array[i, j].mineNeighbours = neighboursCount;
                    neighboursCount = 0;
                }
            }
        }

        public static void CheckBlankNeighbours(Cell[,] test, int X, int Y)
        {
            foreach (int row in neighboursLocations)
            {
                foreach (int col in neighboursLocations)
                {
                    try
                    {
                        if (test[Y + row, X + col].mineNeighbours == 0 && !test[Y + row, X + col].isOpen && !test[Y + row, X + col].isMine)
                        {
                            test[Y + row, X + col].isOpen = true;
                            CheckBlankNeighbours(test, X + col, Y + row);
                        }
                        else if(test[Y + row, X + col].mineNeighbours != 0 && !test[Y + row, X + col].isMine)
                        {
                            test[Y + row, X + col].isOpen = true;
                        }
                    }
                    catch (Exception) { };
                }
            }
        }

        public static void DisplayNeighboursNumberTest()
        {
            for (int i = 0; i < Board.gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < Board.gameBoard.GetLength(1); j++)
                {
                    Console.Write(Board.gameBoard[i, j].mineNeighbours + " ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine();
        }

        public static void DisplayMine()
        {
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" |");
        }

        public static void DisplayOpenCell(int yAxis, int xAxis)
        {
            Console.Write($" {Board.gameBoard[yAxis, xAxis].mineNeighbours} |");
        }

        public static void DisplayClosedCell(int yAxis, int xAxis)
        {
            Console.Write(" # |");
        }
    }
}
