using SG_Bank.BLL;
using SG_Bank.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.UI.Workflows
{
    public class AccountLookupWorkflow
    { 
        public void Execute()
        {
            AccountManager manager = AccountManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Lookup an account");
            Console.WriteLine("______________________");
            Console.Write("Enter an account number: ");
            string accountNumber = Console.ReadLine();

            AccountLookupResponse response = manager.LookupAccount(accountNumber);

            if(response.Success)
            {
                ConsoleIO.DisplayAccountDetails(response.Account);
            }

            else
            {
                Console.WriteLine("An error occurred:   ");
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
