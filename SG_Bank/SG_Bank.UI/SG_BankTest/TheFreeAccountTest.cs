using NUnit.Framework;
using SG_Bank.BLL;
using SG_Bank.BLL.DepositRules;
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
    public class TheFreeAccountTest
    { 
    
        [Test]

    
        [TestCase ("12345", "FreeAccount", 100, AccountType.Free, 250, false)]
        [TestCase ("12345", "FreeAccount", 100, AccountType.Free, -100, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Basic, 50, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, 50, true)]

        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {

            IDeposit depositTest = new FreeAccountDepositRule();

            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = accountType};

            AccountDepositResponse result = depositTest.Deposit(account, amount);
           
            var actual = result.Success;
            Assert.AreEqual(expectedResult, actual);

        }

        [TestCase("12345", "FreeAccount", 100, AccountType.Free, 250, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, -100, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Basic, 50, false)]
        [TestCase("12345", "FreeAccount", 100, AccountType.Free, 50, true)]
        [TestCase("12345", "FreeAccount", 400, AccountType.Free, -400, false)]
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw withdrawTest = new FreeAccountWithdrawRule();

            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = accountType };

            AccountWithdrawResponse result = withdrawTest.Withdraw(account, amount);

            var actual = result.Success;
            Assert.AreEqual(expectedResult, actual);


        }

    }
}
