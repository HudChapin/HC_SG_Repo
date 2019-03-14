using SG_Bank.BLL;
using SG_Bank.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.UI.Workflows
{
    public class DepositWorkflow
    {
        public void Execute()
        {
            Console.Clear();

            AccountManager accountManager = AccountManagerFactory.Create();

            Console.Write("Enter an account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter a deposit amount (Exmaple format (50.00) : ");
            decimal amount = decimal.Parse(Console.ReadLine());            

            AccountDepositResponse response = accountManager.Deposit(accountNumber, amount);

            if (response.Success)
            {
                Console.WriteLine("Deposit Completed!");
                Console.WriteLine($"Account number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old Balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount Deposited: {response.Amount:c}");
                Console.WriteLine($"New Balance: {response.Account.Balance:c}");
            }

            else
            {
                Console.WriteLine("An error occured: ");
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();

        }

    }
    
}
