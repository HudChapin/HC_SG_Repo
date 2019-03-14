using BattleShip.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class Gameflow
    {
        public void StartGame()
        {
            Boolean PlayAgain = true;

            while (PlayAgain)
            {
                ConsoleIO.DisplaySplash();

                GameSetup NewGame = new GameSetup();

                NewGame.CreateBoard();

                ShotStatus Bob = new ShotStatus();

                while (Bob != ShotStatus.Victory)
                {
                    Random rnd = new Random();
                    int RandomlyChoosePlayer = rnd.Next(1, 3);

                    if (RandomlyChoosePlayer == 1)
                    {
                        Console.WriteLine($"{NewGame.Player1.Name}. It is your turn.");
                        NewGame.DisplayShotTracker(NewGame.Player2);
                        Bob = NewGame.PlayerOneTakeTurn();
                        NewGame.DisplayShotTracker(NewGame.Player2);
                        Console.WriteLine("Press any key to contuniue to next players turn");
                        Console.ReadKey();
                        Console.Clear();
                        RandomlyChoosePlayer = 2;
                    }

                    if (RandomlyChoosePlayer == 2)
                    {
                        Console.WriteLine($"{NewGame.Player2.Name}. It is your turn.");
                        NewGame.DisplayShotTracker(NewGame.Player1);
                        Bob = NewGame.PlayerTwoTakeTurn();
                        NewGame.DisplayShotTracker(NewGame.Player1);
                        Console.WriteLine("Press any key to contuniue to next players turn");
                        Console.ReadKey();
                        Console.Clear();
                        RandomlyChoosePlayer = 1;
                    }
                }
            }
            Console.WriteLine("Would you like to continue playing? (Y or N)");
            string Continue = Console.ReadLine();
            string Answer = Continue.ToUpper();

            if (Answer == "Y")
            {
                PlayAgain = true;
            }

            else if (Answer == "N")
            {
                PlayAgain = false;
            }

            else
            {
                PlayAgain = false;
            }
        }
    }
}




