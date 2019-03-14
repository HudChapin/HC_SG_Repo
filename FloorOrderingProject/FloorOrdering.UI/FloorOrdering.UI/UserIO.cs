using FloorOrdering.Models.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.UI
{
    public class UserIO
    {
        public static int GetID()
        {
            int orderID;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter the ID of the order you would like to lookup :");
                string input = Console.ReadLine();

                if (int.TryParse(input, out orderID))
                {
                    return orderID;
                }

                Console.WriteLine("That was an invalid input.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            }
        }

        public static DateTime GetFutureDateFromUser(string prompt)

        {
            DateTime date;

            Console.WriteLine(prompt);

            while (!DateTime.TryParse(Console.ReadLine(), CultureInfo.GetCultureInfo("en-us"), DateTimeStyles.NoCurrentDateDefault, out date)
                || date < DateTime.Today)

            {
                Console.WriteLine("The date you entered was invalid. It must be in the future and format should match example. (example \"MM-DD-YYYY\") Please try again :");
            }
            Console.Clear();
            return date;
        }
        public static DateTime GetDateFromUser(string prompt)

        {                     
            DateTime date;

            Console.WriteLine(prompt);

            while (!DateTime.TryParse(Console.ReadLine(), CultureInfo.GetCultureInfo("en-us"), DateTimeStyles.NoCurrentDateDefault, out date) 
                || date < DateTime.Today.AddYears(-10) )

            {
                Console.WriteLine("The date you entered was invalid. (example \"MM-DD-YYYY\") Please try again :");
            }
            Console.Clear();
            return date;
        }

        public static string GetStringFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter valid text!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
                Console.Clear();
            }
        }

        public static string GetStringFromUserCanBeNull(string prompt)
        {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                
            return input;
        }


        public static decimal GetAreaFromUser(string prompt)

        {
            decimal Decimal;

            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (!decimal.TryParse(input, out Decimal))
                {
                    Console.WriteLine("You must enter a valid decimal.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                else
                {
                    if (Decimal < 100)
                    {
                        Console.WriteLine("The Area must be greater than 100 Sq feet");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    if (Decimal > 100)
                    {
                        Console.Clear();
                        return Decimal;
                    }
                }
            }
        }

        public static decimal GetAreaFromUserCanBe0(string prompt)
        {
            decimal Decimal;

            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (input == string.Empty)
                {
                    return 0;
                }

                if (!decimal.TryParse(input, out Decimal))
                {
                    Console.WriteLine("You must enter a valid decimal.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

                else
                {

                    if (Decimal < 100 )
                    {
                        Console.WriteLine("The Area must be greater than 100 Sq feet");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if (Decimal > 100)
                    {
                        Console.Clear();
                        return Decimal;

                    }
                }
            }
        }

        public static Order GetOrderFromUser(string prompt, List<Order> orders)
        {
            
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int output))
                {
                    if (orders.FirstOrDefault(t => t.OrderNumber == output) != null)
                    {
                        return orders.FirstOrDefault(t => t.OrderNumber == output);
                    }

                    else
                    {
                        Console.WriteLine("The ID you entered does not match any ID for the the specified date. Press any key to try again.");
                        Console.ReadKey();
                        
                    }
                }

                else
                {
                    Console.WriteLine("You must enter a valid integer");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                }
            }
        }

        public static string GetYesOrNoFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt + "(Y/N)");
                string input = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter either Y or N!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.WriteLine("You must enter either Y or N!");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                        continue;
                    }
                    return input;
                }
            }


        }
    }
}
