using SG_Bank.Data;
using SG_Bank.Models;
using SG_Bank.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.UI
{
    public class Menu
    {
        public static void Start()
        {
            FileAccountRepository fileRepo = new FileAccountRepository();

            List<Account> accounts = fileRepo.GetAllAccounts();
         
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SG Bank Application");
                Console.WriteLine("________________________");
                Console.WriteLine("1. Lookup an Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");

                Console.WriteLine("\nQ to quit");
                Console.WriteLine("\nEnter selection");

                string userinput = Console.ReadLine();

                switch(userinput)
                {
                    case "1":
                        AccountLookupWorkflow lookupWorkflow = new AccountLookupWorkflow();
                        lookupWorkflow.Execute();
                        break;
                    case "2":
                        DepositWorkflow depositWorkflow = new DepositWorkflow();
                        depositWorkflow.Execute();
                        fileRepo.SaveAllAccounts(accounts);
                        break;
                    case "3":
                        WithdrawWorkflow withdrawWorkflow = new WithdrawWorkflow();
                        withdrawWorkflow.Execute();
                        fileRepo.SaveAllAccounts(accounts);
                        break;
                    case "Q":
                        
                        return;
                }
            }
        }
    }
}

