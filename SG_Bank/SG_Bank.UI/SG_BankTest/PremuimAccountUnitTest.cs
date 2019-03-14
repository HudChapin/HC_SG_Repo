using NUnit.Framework;
using SG_Bank.BLL.DepositRules;
using SG_Bank.BLL.WithdrawRules;
using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using SG_Bank.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BankTest
{
    [TestFixture]
    public class PremuimAccountUnitTest
    {
        [Test]
        [TestCase("33333", "PremiumAccount", 100, AccountType.Premium, 250, true)]
        [TestCase("33333", "PremiumAccount", 100, AccountType.Premium, 500000, true)]
        [TestCase("33333", "PremiumAccount", 100, AccountType.Premium, -250, false)]
        [TestCase("33333", "PremiumAccount", 100, AccountType.Free, 100, false)]

        public void PremiumAccountDepositTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit premiumDeposit = new NoLimitDepositRule();

            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = accountType };

            AccountDepositResponse result = premiumDeposit.Deposit(account, amount);

            var actual = result.Success;
            var actual2 = account.Balance;

            Assert.AreEqual(expectedResult, actual);          
        }

        [TestCase("33333", "PremiumAccount", 100, AccountType.Free, 100, 100, false)]
        [TestCase("33333", "PremiumAccount", 100, AccountType.Premium, -100, 100, false)]
        [TestCase("33333", "PremiumAccount", 150, AccountType.Premium, 50, 100, true)]
        [TestCase("33333", "PremiumAccount", 100, AccountType.Premium, 500, -400, true)]
        [TestCase("33333", "PremiumAccount", 100, AccountType.Premium, 1000, 100, false)]

        public void PremiumAccountWithdrawTests(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw premiumWithdraw = new PremiumAccountWithdrawRule();

            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = accountType };

            AccountWithdrawResponse result = premiumWithdraw.Withdraw(account, amount);

            var actual = result.Success;
            var actual2 = account.Balance;

            Assert.AreEqual(expectedResult, actual);
            Assert.AreEqual(newBalance, actual2);
        }
    }
}
