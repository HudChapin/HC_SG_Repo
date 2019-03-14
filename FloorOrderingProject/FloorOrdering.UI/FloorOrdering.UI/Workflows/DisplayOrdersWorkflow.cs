using FloorOrdering.BLL;
using FloorOrdering.Data.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.UI.Workflows
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()

        {
            OrderManager manager = OrderManagerFactory.Create();            

            DateTime date = UserIO.GetDateFromUser("Enter the date the order was placed (example \"MM-DD-YYYY\"):");

            DisplayOrderResponse response = manager.OrderLookup(date);

            if (response.Success)
            {               
                ConsoleIO.DisplayOrderDetails(response.Order, response.Date);
            }

            else
            {
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

}