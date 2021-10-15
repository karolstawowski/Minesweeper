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

        public static DifficultyLevel.DifficultyLevelsEnum gameDifficulty;

        public static void SetGameDifficulty()
        {
            char userInput = Char.MinValue;

            Console.WriteLine("Select difficulty level: ");
            Console.WriteLine("[(B)eginner, (I)ntermediate, (E)xprert]");
            
            while (userInput != 'B' && userInput != 'I' && userInput != 'E')
            {
                try
                {
                    userInput = Char.ToUpper(Convert.ToChar(Console.ReadLine()));

                    if(userInput != 'B' && userInput != 'I' && userInput != 'E')
                    {
                        throw new ArgumentException("Invalid character");
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
                            Game.gameDifficulty = DifficultyLevel.DifficultyLevelsEnum.Beginner;
                        }
                        break;
                    case 'I':
                        {
                            Game.gameDifficulty = DifficultyLevel.DifficultyLevelsEnum.Intermediate;
                        }
                        break;
                    case 'E':
                        {
                            Game.gameDifficulty = DifficultyLevel.DifficultyLevelsEnum.Expert;
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
            Console.WriteLine("Mines to find: " + Board.GetMinesNumber((int)gameDifficulty));
        }

        public static void CheckUserLocationInput(ref int input, char inputType)
        {
            int boardAxisMeasurement = 0;
            bool isParsed;

            if (inputType == 'x')
            {
                boardAxisMeasurement = Board.BoardWidth;
            }
            else if (inputType == 'y')
            {
                boardAxisMeasurement = Board.BoardHeight;
            }
            while (input < 0 || input >= boardAxisMeasurement)
            {
                isParsed = int.TryParse(Console.ReadLine(), out int number);
                if(isParsed && number > 0 && number <= boardAxisMeasurement)
                {
                    input = number - 1;
                    return;
                }

                Console.WriteLine("Invalid value");
            }
        }

        
        public static void PlayGame()
        {
            Game.isOver = false;

            Console.Title = "Minesweeper by Karol Stawowski";

            Console.SetWindowSize(44, 8);
            Console.SetBufferSize(44, 8);

            Game.SetGameDifficulty();

            Console.Clear();

            Board.SetBoardSize();

            Board.InitializeBoard();

            Board.AssingMines();

            Cell.GetMinesAround(Board.GameBoard);

            DisplayBoard.ChangeWindowSize(gameDifficulty);

            // Display board for developing purposes
            //DisplayBoard.DisplayMinesLocationTest();
            //Cell.DisplayNeighboursNumberTest();

            Game.DisplayDifficultyLevel();
            DisplayBoard.DisplayOpenAndNeighbouringCells();

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
                if (Board.GameBoard[yAxis, xAxis].IsMine)
                {
                    Game.isOver = true;

                    DisplayBoard.DisplayGameOverBoard();

                    Alert.GameOverAlert();
                }
                // Cell number of neighbouring mines is equal zero - open neighbour cells
                else if (Board.GameBoard[yAxis, xAxis].MinesAround == 0)
                {
                    Cell.CheckEmptyCellsAround(Board.GameBoard, xAxis, yAxis);

                    if (DisplayBoard.DisplayOpenAndNeighbouringCells() == Board.GetMaxOpenCellsNumber((int)gameDifficulty))
                    {
                        Game.isOver = true;
                        break;
                    }
                }
                // Cell has >0 neighbouring mines
                else
                {
                    Board.GameBoard[yAxis, xAxis].IsOpen = true;

                    if (DisplayBoard.DisplayOpenAndNeighbouringCells() == Board.GetMaxOpenCellsNumber((int)gameDifficulty))
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
