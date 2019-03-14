using FloorOrdering.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.UI
{
    class Menu
    {
        public static void Start()
        {
            while (true)
            {
                    Console.Clear();
                    Console.WriteLine("*************************************");
                    Console.WriteLine("          Floor Ordering Menu");
                    Console.WriteLine("*************************************");
                    Console.WriteLine("1. Display Orders");
                    Console.WriteLine("2. Add an Order");
                    Console.WriteLine("3. Edit an Order");
                    Console.WriteLine("4. Remove an Order");                   
                    Console.WriteLine("\nQ to quit");
                    Console.WriteLine("\nEnter selection");

                    string userinput = Console.ReadLine();

                    switch (userinput.ToUpper())
                    {
                        case "1":
                             Console.Clear();
                             DisplayOrdersWorkflow display = new DisplayOrdersWorkflow();
                             display.Execute();
                             break;
                        case "2":
                             Console.Clear();
                             AddOrderWorkflow add = new AddOrderWorkflow();
                             add.Execute();
                            break;
                        case "3":
                             EditOrderWorkflow edit = new EditOrderWorkflow();
                             edit.Execute();
                             break;
                         case "4":
                        RemoveOrderWorkflow remove = new RemoveOrderWorkflow();
                        remove.Execute();
                            break;
                        case "Q":
                            return;
                    }
                
            }
        }
    }
}
