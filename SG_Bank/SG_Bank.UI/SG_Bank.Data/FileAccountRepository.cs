using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_Bank.Models.Interfaces;
using SG_Bank.Models;

namespace SG_Bank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        public Account LoadAccount(string AccountNumber)
        {
            foreach (Account account in GetAllAccounts())
            {
                if (account.AccountNumber == AccountNumber)
                {
                    return account;
                }
            }
            return null;
        }

        public void SaveAccount(Account account)
        {
            List<Account> accounts = GetAllAccounts();

            foreach (Account acct in accounts)
            {
                if (acct.AccountNumber == account.AccountNumber)
                {
                    acct.Balance = account.Balance;
                }
            }
            SaveAllAccounts(accounts);
        }

        public static Account MapToAccount(string row)
        {
            string[] values = row.Split(',');
            Account account = new Account();
            account.AccountNumber = values[0];
            account.Name = values[1];
            decimal.TryParse(values[2], out decimal result);
            account.Balance =result;
            switch (values[3])
            {
                case "F":
                    account.Type = AccountType.Free;
                    break;
                case "B":
                    account.Type = AccountType.Basic;
                    break;
                case "P":
                    account.Type = AccountType.Premium;
                    break;
            }
            return account;

        }


        public static string MapToRow(Account account)
        {
            string returnValue = "";
            returnValue += account.AccountNumber + ",";
            returnValue += account.Name + ",";
            returnValue += account.Balance + ",";
            returnValue += account.Type.ToString()[0];

            return returnValue;
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (StreamReader sr = new StreamReader("Accounts.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Account acct = MapToAccount(line);
                    accounts.Add(acct);
                }
            }

            return accounts;
        }

        public void SaveAllAccounts(List<Account> accounts)
        {
            using (StreamWriter sw = new StreamWriter("Accounts.txt"))
            {
                foreach (Account account in accounts)
                {
                    sw.WriteLine(MapToRow(account));
                }
            }
        }
    }
}
