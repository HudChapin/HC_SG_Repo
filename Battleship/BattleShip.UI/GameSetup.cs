using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using BattleShip.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class GameSetup
    {
       public Player Player1 = new Player();
       public Player Player2 = new Player();
        public void CreateBoard()
        {

            Player1.Name = ConsoleIO.GetStringFromUser("Player 1 please enter your Name: ");
            Player2.Name = ConsoleIO.GetStringFromUser("Player 2 please enter your Name: ");

            Console.WriteLine($"{Player1.Name} time to place your ships");
            Player1.Board = new Board();
            PlaceShipOnBoard(Player1);
            Console.WriteLine($"{Player2.Name} time to place your ships");
            Player2.Board = new Board();
            PlaceShipOnBoard(Player2);
        }

        public ShotStatus PlayerOneTakeTurn()
        {
            Coordinate Input = ConsoleIO.GetCoordinate();
            FireShotResponse Result = new FireShotResponse();
            Result = Player2.Board.FireShot(Input);
            
            switch (Result.ShotStatus)
            {
                case ShotStatus.Duplicate:
                    Console.WriteLine("You have already shot at this position. Please try again.");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.Invalid:
                    Console.WriteLine("That position is invalid. Please try again.");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.Hit:
                    Console.WriteLine("You an enemy ship!");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.Miss:
                    Console.WriteLine("That was a miss.");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.HitAndSunk:
                    Console.WriteLine("You have sunk the enimies " + Result.ShipImpacted);
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.Victory:
                    Console.WriteLine($"{Player1} is Victorious! All of the enemy ships are sunk");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;
            }
            return Result.ShotStatus;

        }

        public ShotStatus PlayerTwoTakeTurn()
        {

            Coordinate Input = ConsoleIO.GetCoordinate();
            FireShotResponse Result = new FireShotResponse();
            Result = Player1.Board.FireShot(Input);
            switch (Result.ShotStatus)
            {
                case ShotStatus.Duplicate:
                    Console.WriteLine("You have already shot at this position. Please try again.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    break;

                case ShotStatus.Invalid:
                    Console.WriteLine("That position is invalid. Please try again.");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.Hit:
                    Console.WriteLine("You an enemy ship!");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.Miss:
                    Console.WriteLine("That was a miss.");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.HitAndSunk:
                    Console.WriteLine("You have sunk the enimies " + Result.ShipImpacted);
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;

                case ShotStatus.Victory:
                    Console.WriteLine($"{Player2} is Victorious! All of the enemy ships are sunk");
                    Console.WriteLine("Press any key to continue.");
                    Console.Clear();
                    break;
            }
            return Result.ShotStatus;
        }


        public void DisplayShotTracker(Player player)
        {
            string xLetter = "ABCDEFGHIJ";
            char row = 'A';

            Console.WriteLine("    1  2  3  4  5  6  7  8  9  10");
            for (int x = 1; x <= 10; x++)
            {
                row = xLetter[x - 1];
                Console.Write($"\n[{row}]");
                for (int y = 1; y <= 10; y++)
                {
                    ShotHistory history = player.Board.CheckCoordinate(new Coordinate(x, y));
                    switch (history)
                    {
                        case ShotHistory.Hit:
                            Console.ForegroundColor = ConsoleColor.Red; Console.Write("H");
                            break;
                        case ShotHistory.Miss:
                            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("M");
                            break;
                        case ShotHistory.Unknown:
                            Console.Write(" ");
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" |");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" ");
        }


        public static void PlaceShipOnBoard(Player player)
        {
            PlaceDestroyer(player);
            PlaceSubmarine(player);
            PlaceCruiser(player);
            PlaceBattleship(player);
            PlaceCarrier(player);
            Console.ReadKey();
            Console.Clear();
        }

        public static void PlaceDestroyer(Player player)
        {
            while (true)
            {
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Destroyer;
                request.Direction = ConsoleIO.getDirection(ConsoleIO.GetStringFromUser("What direction will you place the Destroyer? (l, r, u, d)"));
                request.Coordinate = ConsoleIO.GetCoordinate();

                ShipPlacement response = new ShipPlacement();
                response = player.Board.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space try again.");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ships are overlapping");
                        continue;
                    case ShipPlacement.Ok:
                        Console.WriteLine("Ship has been placed");
                        break;
                }
                break;
            }
        }

        public static void PlaceSubmarine(Player player)
        {
            while (true)
            {
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Submarine;
                request.Direction = ConsoleIO.getDirection(ConsoleIO.GetStringFromUser("What direction will you place the Submarine? (l, r, u, d)"));
                request.Coordinate = ConsoleIO.GetCoordinate();

                ShipPlacement response = new ShipPlacement();
                response = player.Board.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space try again.");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ships are overlapping");
                        continue;
                    case ShipPlacement.Ok:
                        Console.WriteLine("Ship has been placed");
                        break;
                }
                break;
            }
        }

        public static void PlaceCruiser(Player player)
        {
            while (true)
            {
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Cruiser;
                request.Direction = ConsoleIO.getDirection(ConsoleIO.GetStringFromUser("What direction will you place the Cruiser? (l, r, u, d)"));
                request.Coordinate = ConsoleIO.GetCoordinate();

                ShipPlacement response = new ShipPlacement();
                response = player.Board.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space try again.");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ships are overlapping");
                        continue;
                    case ShipPlacement.Ok:
                        Console.WriteLine("Ship has been placed");
                        break;
                }
                break;
            }
        }

        public static void PlaceBattleship(Player player)
        {
            while (true)
            {
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Battleship;
                request.Direction = ConsoleIO.getDirection(ConsoleIO.GetStringFromUser("What direction will you place the Battleship? (l, r, u, d)"));
                request.Coordinate = ConsoleIO.GetCoordinate();

                ShipPlacement response = new ShipPlacement();
                response = player.Board.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space try again.");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ships are overlapping");
                        continue;
                    case ShipPlacement.Ok:
                        Console.WriteLine("Ship has been placed");
                        break;
                }
                break;
            }
        }

        public static void PlaceCarrier(Player player)
        {
            while (true)
            {
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Carrier;
                request.Direction = ConsoleIO.getDirection(ConsoleIO.GetStringFromUser("What direction will you place the Carrier? (l, r, u, d)"));
                request.Coordinate = request.Coordinate = ConsoleIO.GetCoordinate();

                ShipPlacement response = new ShipPlacement();
                response = player.Board.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space try again.");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ships are overlapping");
                        continue;
                    case ShipPlacement.Ok:
                        Console.WriteLine("Ship has been placed");
                        break;
                }

                break;
            }
        }
    }
}



