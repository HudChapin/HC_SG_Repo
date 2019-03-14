using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI
{
    public class ConsoleIO
    {
        public static void DisplaySplash()
        {
            Console.Clear();
            Console.WriteLine("     =============================");
            Console.WriteLine("     |                            |");
            Console.WriteLine("     |   Welcome To Battleship    |");
            Console.WriteLine("     |                            |");
            Console.WriteLine("     =============================\n");
            Console.WriteLine("Press any key to play");
            Console.ReadKey();
        }

        public static string GetStringFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string Result = Console.ReadLine();
                if (Result == string.Empty)
                {
                    Console.WriteLine("Invalid. Try again.");
                    continue;
                }
                return Result;
            }

        }

        public static ShipDirection getDirection(string direction)
        {
            switch (direction.ToUpper())
            {
                case "U":
                    return ShipDirection.Up;
                case "R":
                    return ShipDirection.Right;
                case "D":
                    return ShipDirection.Down;
                case "L":
                    return ShipDirection.Left;
                default:
                    return ShipDirection.Left;
            }
        }

        public static int GetLetterToNumber(string letter)
        {
            int result = 0;
            switch (letter.ToUpper())
            {
                case "A":
                    result = 1;
                    break;
                case "B":
                    result = 2;
                    break;
                case "C":
                    result = 3;
                    break;
                case "D":
                    result = 4;
                    break;
                case "E":
                    result = 5;
                    break;
                case "F":
                    result = 6;
                    break;
                case "G":
                    result = 7;
                    break;
                case "H":
                    result = 8;
                    break;
                case "I":
                    result = 9;
                    break;
                case "J":
                    result = 10;
                    break;
                default:
                    break;
            }
            return result;
        }

        public static Coordinate GetCoordinate()
        {
            int XCoordinate = 0;
            
            string Coor = GetStringFromUser("Please enter the coordinate to shoot for :");
            string Letter = Coor.Substring(0, 1);
            XCoordinate = GetLetterToNumber(Letter);

            String SecondLetter = Coor.Substring(1);
            Int32.TryParse(SecondLetter, out int YCoordinate);

            Coordinate PlayerCoordinate = new Coordinate(XCoordinate, YCoordinate);
            return PlayerCoordinate;
            
        
        }
    }
}
