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
    public class BasicAccountTest
    {
       [Test]
        [TestCase("33333", "BasicAccount", 100, AccountType.Free, 250, false)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Basic, -100, false)] 
        [TestCase("33333", "BasicAccount", 100, AccountType.Basic, 250, true)] 

        public void BasicAccountDepositTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit basicDeposit = new NoLimitDepositRule();

            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = accountType };

            AccountDepositResponse result = basicDeposit.Deposit(account, amount);

            var actual = result.Success;
            Assert.AreEqual(expectedResult, actual);
        }
        
        [TestCase("33333", "BasicAccount", 1500, AccountType.Basic, 1000, 1500, false)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Free, 100, 100, false)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Basic, -100, 100, false)]
        [TestCase("33333", "BasicAccount", 150, AccountType.Basic, 50, 100, true)]
        [TestCase("33333", "BasicAccount", 100, AccountType.Basic, 150, -60, true)]

        public void BasicAccountWithdrawTests(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw basicWithdraw = new BasicAccountWithdrawRule();

            Account account = new Account() { AccountNumber = accountNumber, Name = name, Balance = balance, Type = accountType };

            AccountWithdrawResponse result = basicWithdraw.Withdraw(account, amount);

            var actual = result.Success;
            var actual2 = account.Balance;

            Assert.AreEqual(expectedResult, actual);
            Assert.AreEqual(newBalance, actual2);           
        }
    }
}
