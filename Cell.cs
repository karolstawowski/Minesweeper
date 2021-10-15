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

        int row = -1;
        int column = -1;
        public bool IsOpen = false;
        public bool IsMine;
        public int MinesAround;

        public Cell(bool isOpen, int row, int column, bool isMine, int minesAround)
        {
            this.IsOpen = isOpen;
            this.row = row;
            this.column = column;
            this.IsMine = isMine;
            this.MinesAround = minesAround;
        }
        public static void GetMinesAround(Cell[,] array)
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
                                if (array[i + row, j + col].IsMine)
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
                    array[i, j].MinesAround = neighboursCount;
                    neighboursCount = 0;
                }
            }
        }

        public static void CheckEmptyCellsAround(Cell[,] test, int X, int Y)
        {
            foreach (int row in neighboursLocations)
            {
                foreach (int col in neighboursLocations)
                {
                    try
                    {
                        if (test[Y + row, X + col].MinesAround == 0 && !test[Y + row, X + col].IsOpen && !test[Y + row, X + col].IsMine)
                        {
                            test[Y + row, X + col].IsOpen = true;
                            CheckEmptyCellsAround(test, X + col, Y + row);
                        }
                        else if(test[Y + row, X + col].MinesAround != 0 && !test[Y + row, X + col].IsMine)
                        {
                            test[Y + row, X + col].IsOpen = true;
                        }
                    }
                    catch (Exception) { };
                }
            }
        }
    }
}
