using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class Game
    {
        static bool isOver = false;

        public static DifficultyLevels.DifficultyLevelsEnum gameDifficulty;

        public static void SetDifficulty()
        {
            Console.WriteLine("Select difficulty level: ");
            Console.WriteLine("[(B)eginner, (I)ntermediate, (E)xprert]");
            char userInput = Char.MinValue;
            
            while (userInput != 'B' && userInput != 'I' && userInput != 'E')
            {
                try
                {
                    userInput = Char.ToUpper(Convert.ToChar(Console.ReadLine()));

                    if(userInput != 'B' && userInput != 'I' && userInput != 'E')
                    {
                        throw new Exception("Invalid character");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid character");
                }
                switch (userInput)
                {
                    case 'B':
                        {
                            Game.gameDifficulty = DifficultyLevels.DifficultyLevelsEnum.Beginner;
                        }
                        break;
                    case 'I':
                        {
                            Game.gameDifficulty = DifficultyLevels.DifficultyLevelsEnum.Intermediate;
                        }
                        break;
                    case 'E':
                        {
                            Game.gameDifficulty = DifficultyLevels.DifficultyLevelsEnum.Expert;
                        }
                        break;
                }
            }
        }
        public static void DisplayDifficultyLevel()
        {
            Console.WriteLine($"Difficulty level: {gameDifficulty}" );
            DisplayMinesToFind();
            Console.WriteLine();
        }

        public static void DisplayMinesToFind()
        {
            Console.WriteLine("Mines to find: " + Board.minesNumbers[(int)gameDifficulty]);
        }

        public static void CheckUserLocationInput(ref int input, char inputType)
        {
            int boardAxisMeasurement = 0;
            if (inputType == 'x')
            {
                boardAxisMeasurement = Board.boardWidth;
            }
            else if (inputType == 'y')
            {
                boardAxisMeasurement = Board.boardHeight;
            }
            while (input < 0 || input > boardAxisMeasurement)
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (input < 0 || input > boardAxisMeasurement)
                    {
                        throw new Exception("Invalid value");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid value");
                };
            }
        }

        public static void ChangeWindowSize(DifficultyLevels.DifficultyLevelsEnum difficultyLevel)
        {
            if((int)difficultyLevel == 0)
            {
                Console.SetWindowSize(34, 28);
            }
            else if ((int)difficultyLevel == 1)
            {
                Console.SetWindowSize(42, 32);
            }
            else if((int)difficultyLevel == 2)
            {
                Console.SetWindowSize(70, 46);
            }
        }
        public static void PlayGame()
        {
            Game.isOver = false;

            Console.Title = "Minesweeper by Karol Stawowski";

            Console.SetWindowSize(44, 8);

            Game.SetDifficulty();

            Console.Clear();

            ChangeWindowSize(gameDifficulty);

            Board.SetBoardSize();

            Board.InitializeBoard();

            Board.AssingMines();

            Cell.getNeighbours(Board.gameBoard);

            // Display board for developing purposes
            //Board.DisplayMineLocationsTest();
            //Cell.DisplayNeighboursNumberTest();

            Game.DisplayDifficultyLevel();
            Board.DisplayOpenAndNeighbouringCells();

            do
            {
                Console.WriteLine("Location to check (X,Y): ");
                int yAxis = -1;
                int xAxis = -1;

                Game.CheckUserLocationInput(ref xAxis, 'x');
                Game.CheckUserLocationInput(ref yAxis, 'y');

                Console.Clear();
                Game.DisplayDifficultyLevel();

                // Mine has been clicked - game is lost
                if (Board.gameBoard[yAxis, xAxis].isMine)
                {
                    Game.isOver = true;

                    Board.DisplayGameOverBoard();

                    Alert.GameOverAlert();
                }
                // Cell number of neighbouring mines is equal zero - open neighbour cells
                else if (Board.gameBoard[yAxis, xAxis].mineNeighbours == 0)
                {
                    Cell.CheckBlankNeighbours(Board.gameBoard, xAxis, yAxis);

                    if (Board.DisplayOpenAndNeighbouringCells() == Board.maxOpenCells[(int)gameDifficulty])
                    {
                        Game.isOver = true;
                        break;
                    }
                }
                // Cell has >0 neighbouring mines
                else
                {
                    Board.gameBoard[yAxis, xAxis].isOpen = true;

                    if (Board.DisplayOpenAndNeighbouringCells() == Board.maxOpenCells[(int)gameDifficulty])
                    {
                        Game.isOver = true;
                        Alert.GameWonAlert();
                        break;
                    }
                }
                
            }
            while (!Game.isOver);
        }

        public static void PlayingLoop()
        {
            bool wantToPlay;
            char userInput = Char.MinValue;
            do
            {
                Game.PlayGame();
                Console.WriteLine("Do you want to play a new game?");
                Console.WriteLine("(Y)es or (N)o");

                do
                {
                    try
                    {
                        userInput = Convert.ToChar(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid character");
                    }
                }
                while (userInput is Char.MinValue);

                if (Char.ToUpper(userInput) == 'Y')
                {
                    wantToPlay = true;
                    Console.Clear();
                }
                else wantToPlay = false;
            }
            while (wantToPlay);

            Alert.GoodbyeAlert();
        }
    }
}
