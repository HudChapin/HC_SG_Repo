using FloorOrdering.BLL;
using FloorOrdering.Data.Data;
using FloorOrdering.Data.Responses;
using FloorOrdering.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.UI.Workflows
{
    public class AddOrderWorkflow
    {
        private OrderRepository repo = new OrderRepository();
        public void Execute()
        {
            Console.Clear();
            Console.WriteLine("         Add Order");
            ConsoleIO.DisplaySeparatorBar();

            Order order = new Order();

            int originalOrderNumber = 0;

            order.OrderDate = UserIO.GetFutureDateFromUser("Enter the Order Date. This date must be in the future. :");

            TaxesRepository taxRepo = new TaxesRepository();
           
            ConsoleIO.DisplayTaxRepo(taxRepo.List());

            Taxes tax = null;

            while (tax == null)
            {
                string state = UserIO.GetStringFromUserCanBeNull($"Please enter the state Abbreviation for the customer. :").ToUpper();
                if (state != string.Empty)
                {
                    tax = taxRepo.List().FirstOrDefault(t => t.StateAbbreviation.ToUpper() == state);

                    if (tax != null)
                    {
                        order.State = tax.StateAbbreviation.ToUpper();
                        order.TaxRate = tax.TaxRate;
                        break;
                    }

                    else
                    {
                        Console.WriteLine("We do not do business in that state");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadLine();
                    }
                }

                else
                {
                    Console.WriteLine("This may not be empty. Press any key to try again");
                    Console.ReadKey();
                }
            }

            Console.Clear();

            ProductRepository productRepo = new ProductRepository();

            ConsoleIO.DisplayProductRepo(productRepo.List());

            Product product = null;

            while (product == null)
            {
                string productType = UserIO.GetStringFromUserCanBeNull($"Please enter the product type information for the customer. :").ToUpper();
                if (productType != string.Empty)
                {
                    product = productRepo.List().FirstOrDefault(p => p.ProductType.ToUpper() == productType);

                    if (product != null)
                    {
                        order.CostPerSquareFoot = product.CostPerSquareFoot;
                        order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                        order.ProductType = product.ProductType.ToUpper();
                        break;
                    }

                    if (product == null)
                    {
                        Console.WriteLine("We do not offer that flooring type.");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                    }

                }

                else
                {
                    Console.WriteLine("This may not be empty. Press any key to try again.");
                    Console.ReadKey();
                }

            }
            Console.Clear();

            order.Area = UserIO.GetAreaFromUser("Enter the order area (\"Must be more than 100 square feet\") :");

            while (true)
            {
                string customerName = UserIO.GetStringFromUserCanBeNull($"Please enter a name for the customer. This can only contain Letters, Digits, periods, commas, or spaces. :");

                if (customerName.Trim() == string.Empty)
                {
                    Console.WriteLine("The name customer name cannot be blank. Press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                bool result = customerName.All(c => Char.IsLetterOrDigit(c) || c == '.' || c == ',' || c == ' ');

                if (result == false)
                {
                    Console.WriteLine("The customer name you entered contained an invalid value. Press any key to try again");
                    Console.ReadKey();
                    Console.Clear();
                }

                else
                {
                    order.CustomerName = customerName;
                    break;
                }                
            }
            Console.Clear();

            order.MaterialCost = order.Area * order.CostPerSquareFoot;

            order.LaborCost = order.Area * order.LaborCostPerSquareFoot;

            order.Tax = (order.MaterialCost + order.LaborCost) * (order.TaxRate/100);

            order.Total = order.MaterialCost + order.LaborCost + order.Tax;

            OrderManager manager = OrderManagerFactory.Create();

            order.MaterialCost.ToString(); order.LaborCost.ToString(); order.Tax.ToString(); order.Total.ToString();
            string line = "{0, 5} {1, 10} {2, 12} {3,8} {4, 15} {5, 15} {6, 12} {7, 9} {8, 9}";
            Console.WriteLine(line, "Order ID", "Order Date", "Cust Name", "State", "Prod Type", "Material Cost", "Labor Cost", "Tax", "Total");
            Console.WriteLine("_______________________________________________________________________________________");
            string line2 = "{0, 5} {1, 12} {2, 9} {3, 10} {4, 14} {5, 12} {6, 16} {7, 11} {8, 11}";
            Console.WriteLine(line2, order.OrderNumber, order.OrderDate.ToString("MM/dd/yyyy"), order.CustomerName, order.State, order.ProductType, String.Format("{0:0.00}", order.MaterialCost), String.Format("{0:0.00}", order.LaborCost), String.Format("{0:0.00}", order.Tax), String.Format("{0:0.00}", order.Total));

            string input = UserIO.GetYesOrNoFromUser($"Do you want to add this order new order for {order.CustomerName}?");

            if (input == "Y")
            {
                AddOrderResponse addResponse = manager.OrderAdd(order.OrderDate, order, originalOrderNumber);

                if (addResponse.Success == true)
                {
                    Console.WriteLine("The order has been added.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                }


                else
                {
                    Console.WriteLine(addResponse.Message);
                    Console.WriteLine("The request has been cancelled. Press any key to return to the main menu");
                    Console.ReadKey();
                }
            }

            else
            {
                Console.WriteLine("Add order request has been cancelled. Press any key to return to the main menu.");
                Console.ReadKey();
            }
        }
    }
}
