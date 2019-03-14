using FloorOrdering.BLL;
using FloorOrdering.Data.Data;
using FloorOrdering.Data.Responses;
using FloorOrdering.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            DateTime date = UserIO.GetDateFromUser("Enter the date the order was placed (example \"MM-DD-YYYY\"):");

            DisplayOrderResponse response = manager.OrderLookup(date);


            if (response.Success)
            {
                ConsoleIO.DisplayOrderDetails(response.Order, response.Date);
                Order order = UserIO.GetOrderFromUser("Which order would you like to remove? (Use Order ID) :", response.Order);

                Console.Clear();

                order.MaterialCost.ToString(); order.LaborCost.ToString(); order.Tax.ToString(); order.Total.ToString();

                string line = "{0, 5} {1, 10} {2, 12} {3,8} {4, 15} {5, 15} {6, 12} {7, 9} {8, 9}";
                Console.WriteLine(line, "Order ID", "Order Date", "Cust Name", "State", "Prod Type", "Material Cost", "Labor Cost", "Tax", "Total");
                Console.WriteLine("_______________________________________________________________________________________");
                string line2 = "{0, 5} {1, 12} {2, 9} {3, 10} {4, 14} {5, 12} {6, 16} {7, 11} {8, 11}";
                Console.WriteLine(line2, order.OrderNumber, date.ToString("MM/dd/yyyy"), order.CustomerName, order.State, order.ProductType, String.Format("{0:0.00}", order.MaterialCost), String.Format("{0:0.00}", order.LaborCost), String.Format("{0:0.00}", order.Tax), String.Format("{0:0.00}", order.Total));

                
                string input = null;

                while (input == null)
                {
                    input = UserIO.GetYesOrNoFromUser("Do you want to remove this order?");

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("You must enter either Y or N");
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();
                        input = null;
                    }

                    else
                    {
                        if (input != "Y" && input != "N")
                        {
                            Console.WriteLine("You must enter either Y or N");
                            Console.WriteLine("Press any key to try again");
                            Console.ReadKey();
                            input = null;
                        }

                        else if (input == "Y")
                        {
                            RemoveOrderResponse removeResponse = manager.OrderRemove(date, order.OrderNumber, order);

                            if (removeResponse.Success == true)
                            {
                                Console.WriteLine(removeResponse.Message);
                                Console.ReadKey();
                            }

                            else
                            {
                                Console.WriteLine(removeResponse.Message);
                                Console.ReadKey();
                            }

                        }

                        else if (input == "N")
                        {
                            Console.WriteLine("You have chosen to cancel the remove order request. It will not be removed. Press any to return to the main menuc");
                            Console.ReadKey();

                        }
                    }
                }
            }

            else
            {
                Console.WriteLine("There are no orders from that date.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            Console.Clear();
        }
    }
}
