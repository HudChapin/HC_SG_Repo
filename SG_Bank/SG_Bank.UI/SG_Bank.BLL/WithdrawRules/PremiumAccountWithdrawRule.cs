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
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: a non-premium account hit the Premium Withdraw Rule. Contact IT";
                return response;
            }

            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Withdrawl amounts must be greater than zero";
                return response;
            }

            if (account.Balance - amount < -500)
            {
                response.Success = false;
                response.Message = " This amount with overdraft more than your $500.00 limit";
                return response;
            }
            //was unable to get it to display a negative amount when the account is overdrawn
            response.OldBalance = account.Balance;
            response.Amount = amount;
            account.Balance -= amount;
            response.Account = account;
            response.Success = true;

            return response;
        }
    }
}
