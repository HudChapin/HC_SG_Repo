using SG_Bank.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Models.Interfaces
{
    public interface IWithdraw
    {
        AccountWithdrawResponse Withdraw(Account account, decimal amount);
    }
}
