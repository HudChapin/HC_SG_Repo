using SG_Bank.BLL.WithdrawRules;
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
    public class WithdrawRulesFactory
    {
        public static IWithdraw Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.Free:
                    return new FreeAccountWithdrawRule();
                case AccountType.Basic:
                    return new BasicAccountWithdrawRule();
                case AccountType.Premium:
                    return new PremiumAccountWithdrawRule();
            }
            throw new Exception("Account type is not supported");
        }

    }
}
