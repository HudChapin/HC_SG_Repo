using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using SG_Bank.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.BLL.DepositRules
{
    public class FreeAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {

            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if(account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: a non free account hit the free Deposit rule. Contact IT";
                return response;
            }

            if(amount <= 0)
            {
                response.Success = false;
                response.Message = "Withdraw amount must be positive";
                return response;
            }

            if(amount>100)
            {
                response.Success = false;
                response.Message = "Free accounts cannot withdraw more than $100.00";
                return response;
            }

            if(account.Balance + amount < 0)
            {
                response.Success = false;
                response.Message = "Free accounts cannot overdraft!";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance -= amount;
            response.Account = account;
            response.Amount = amount;
            response.Success = true;

            return response;
        }
    }
}
