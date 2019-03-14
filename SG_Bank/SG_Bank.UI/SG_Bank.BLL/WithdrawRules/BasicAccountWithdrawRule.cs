using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using SG_Bank.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.BLL.WithdrawRules
{
    public class BasicAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if(account.Type != AccountType.Basic)
            {
                response.Success = false;
                response.Message = "Error: a non-basic account hit the Basic Withdraw Rule. Contact IT";
                return response;
            }

            if(amount <= 0)
            {
                response.Success = false;
                response.Message = "Withdrawl amounts must be greater than zero";
                return response;
            }

            if (amount > 500)
            {
                response.Success = false;
                response.Message = "Basic accounts cannot withdraw more than $500.00";
                return response;
            }

            if (account.Balance - amount < -100)
            {
                response.Success = false;
                response.Message = " This amount with overdraft more than your $100.00 limit";
                return response;
            }
            if (account.Balance - amount < 0)
            {
                //Can't get this to display the new balance as a negative in this case... Moving on for nowc
                response.OldBalance = account.Balance;
                account.Balance -= amount;
                account.Balance -= 10;
                response.Account = account;
                response.Amount = amount;
                response.Success = true;

                return response;
            }

            response.OldBalance = account.Balance;
            response.Amount = amount;
            account.Balance -= amount;
            response.Account = account;          
            response.Success = true;

            return response;
        }
    }
}
