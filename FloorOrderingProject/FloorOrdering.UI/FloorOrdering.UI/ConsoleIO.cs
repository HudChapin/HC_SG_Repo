using FloorOrdering.Models.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.UI
{
    public class ConsoleIO
    { 
        public static void DisplaySeparatorBar()
        {
            Console.WriteLine("__________________________________________________________________________________________________________________");
        }

        public static void DisplaySeparatorBar2()
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public const string OrderLineFormat = "{0, 5} {1, 12} {2, 9} {3, 10} {4, 14} {5, 12} {6, 16} {7, 11} {8, 11}";

        public static void DisplayOrderDetails(List<Order> orders, DateTime date)
        {
            foreach (Order order in orders)
            {
                order.MaterialCost.ToString(); order.LaborCost.ToString(); order.Tax.ToString(); order.Total.ToString();
                DisplayOrderHeader();
                string line = "{0, 5} {1, 12} {2, 9} {3, 10} {4, 14} {5, 12} {6, 16} {7, 11} {8, 11}";
                Console.WriteLine(line, order.OrderNumber, date.ToString("MM/dd/yyyy"), order.CustomerName, order.State, order.ProductType, String.Format("{0:0.00}", order.MaterialCost), String.Format("{0:0.00}", order.LaborCost), String.Format("{0:0.00}", order.Tax), String.Format("{0:0.00}", order.Total));
                DisplaySeparatorBar();
            }
        }

        public static void DisplayOrderHeader()
        {
            string line = "{0, 5} {1, 10} {2, 12} {3,8} {4, 15} {5, 15} {6, 12} {7, 9} {8, 9}";
            DisplaySeparatorBar();
            Console.WriteLine(line, "Order ID", "Order Date", "Cust Name", "State", "Prod Type", "Material Cost", "Labor Cost", "Tax", "Total");
            DisplaySeparatorBar2();
        }

        public static void DisplayTaxRepo(List<Taxes> taxes)
        {
            string line = "{0, 5}, {1, 5}, {2, 5}";

            foreach (Taxes tax in taxes)
            {
                Console.WriteLine(line, "State Abbreviation", "State Name", "Tax Rate");
                DisplaySeparatorBar2();
                Console.WriteLine(line, tax.StateAbbreviation, tax.StateName, tax.TaxRate);
                Console.WriteLine();
            }
            DisplaySeparatorBar();
        }

        public static void DisplayProductRepo(List<Product> products)
        {
            string line = "{0, 5}, {1, 5}, {2, 5}";

            foreach (Product product in products)
            {
                Console.WriteLine(line, "Product Type", "Cost Per Square Foot", "Labor Cost Per Square Foot");
                DisplaySeparatorBar2();
                Console.WriteLine(line, product.ProductType, product.CostPerSquareFoot, product.LaborCostPerSquareFoot);
                Console.WriteLine();
            }
            DisplaySeparatorBar();
        }
    }
}
