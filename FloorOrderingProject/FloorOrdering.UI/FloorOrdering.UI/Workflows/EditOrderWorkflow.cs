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
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            DateTime date = UserIO.GetDateFromUser("Enter the date the order was placed (example \"MM-DD-YYYY\"):");

            DisplayOrderResponse response = manager.OrderLookup(date);
            Console.Clear();


            if (response.Success)
            {
                Order order = null;
                while (order == null)
                {
                    ConsoleIO.DisplayOrderDetails(response.Order, response.Date);
                    order = UserIO.GetOrderFromUser("Which order would you like to edit? (Use Order ID) :", response.Order);
                    if (order == null)
                    {
                        Console.WriteLine("The input did not match an order ID");
                    }

                    else
                    {
                        break;
                    }
                }

                while (true)
                {
                    string customerName = UserIO.GetStringFromUserCanBeNull($"Please enter a new name for the customer. The current customer name is {order.CustomerName}. If you do no wish to change this field hit enter without entering any data. :");

                    if (customerName.Trim() == string.Empty)
                    {
                        break;
                    }

                    bool result = order.CustomerName.All(c => Char.IsLetterOrDigit(c) || c == '.' || c == ',' || c == ' ');

                    if (result == false)
                    {
                        Console.WriteLine("The customer name you entered contained an invalid value");
                        Console.ReadLine();
                    }

                    else
                    {
                        order.CustomerName = customerName;
                        break;
                    }
                }

                Console.Clear();
                TaxesRepository taxRepo = new TaxesRepository();

                ConsoleIO.DisplayTaxRepo(taxRepo.List());

                Taxes tax = null;

                while (tax == null)
                {
                    string state = UserIO.GetStringFromUserCanBeNull($"Please enter the new state Abbreviation for the customer. The current state is {order.State}.If you do no wish to change this field hit enter without entering any data. :").ToUpper();
                    if (state != string.Empty)
                    {
                        tax = taxRepo.List().FirstOrDefault(t => t.StateAbbreviation.ToUpper() == state);

                        if (tax != null)
                        {
                            order.State = tax.StateAbbreviation;
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
                        break;
                    }
                }

                Console.Clear();
                ProductRepository productRepo = new ProductRepository();

                ConsoleIO.DisplayProductRepo(productRepo.List());

                Product product = null;

                while (product == null)
                {
                    string productType = UserIO.GetStringFromUserCanBeNull($"Please enter the new product type information for the customer. The current product type is {order.ProductType} (Carpet, Laminate, Tile, Wood). If you do no wish to change this field hit enter without entering any data. :").ToUpper();
                    if (productType != string.Empty)
                    {
                        product = productRepo.List().FirstOrDefault(p => p.ProductType.ToUpper() == productType);

                        if (product != null)
                        {
                            order.CostPerSquareFoot = product.CostPerSquareFoot;
                            order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                            order.ProductType = product.ProductType;
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
                        break;
                    }

                }

                Console.Clear();

                while (true)
                {
                    decimal area = UserIO.GetAreaFromUserCanBe0($"Please enter the new area information for this order. The current area is {order.Area}. If you do no wish to change this field hit enter without entering any data. :");
                    if (area == 0)
                    {
                        break;
                    }

                    else
                    {
                            order.Area = area;                           
                            break;

                    }
                }
                order.MaterialCost = order.Area * order.CostPerSquareFoot;
                order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
                order.Total = order.MaterialCost + order.LaborCost + order.Tax;
                order.Tax = (order.MaterialCost + order.LaborCost) * (order.TaxRate / 100);
                order.OrderDate = response.Date;

                order.MaterialCost.ToString(); order.LaborCost.ToString(); order.Tax.ToString(); order.Total.ToString();
                string line = "{0, 5} {1, 10} {2, 12} {3,8} {4, 15} {5, 15} {6, 12} {7, 9} {8, 9}";
                Console.WriteLine(line, "Order ID", "Order Date", "Cust Name", "State", "Prod Type", "Material Cost", "Labor Cost", "Tax", "Total");
                Console.WriteLine("_______________________________________________________________________________________");
                string line2 = "{0, 5} {1, 12} {2, 9} {3, 10} {4, 14} {5, 12} {6, 16} {7, 11} {8, 11}";
                Console.WriteLine(line2, order.OrderNumber, order.OrderDate.ToString("MM/dd/yyyy"), order.CustomerName, order.State, order.ProductType, String.Format("{0:0.00}", order.MaterialCost), String.Format("{0:0.00}", order.LaborCost), String.Format("{0:0.00}", order.Tax), String.Format("{0:0.00}", order.Total));

                string input = UserIO.GetYesOrNoFromUser($"Do you want to save these changes for {order.CustomerName}?");

                if (input == "Y")
                {
                    EditOrderResponse editResponse = manager.OrderEdit(order);

                    if (editResponse.Success == true)
                    {
                        Console.WriteLine("The order has been added.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                    }


                    else
                    {
                        Console.WriteLine(editResponse.Message);
                        Console.WriteLine("The request has been cancelled. Press any key to return to the main menu");
                        Console.ReadKey();
                    }
                }

                else
                {
                    Console.WriteLine("You have chosen not to save the changes to the order. Press any key to return to the main menu.");
                    Console.ReadKey();
                }

             
            }
            else
            {
                Console.WriteLine("There are no orders from that date.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

    }
}

