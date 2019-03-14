using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrdering.BLL;
using FloorOrdering.Data.Responses;
using FloorOrdering.Models.Models;
using NUnit.Framework;

namespace FloorOrdering.Tests
{
    [TestFixture]
    public class BLLTests
    {
        [Test]
        [TestCase("valid", "OH", "Carpet", 150.00, true)]
        [TestCase("not valid", "OH", "Carpet", 10.00, false)]
        [TestCase("not valid", "OH", "not valid", 150.00, false)]
        [TestCase("not valid%", "OH", "Carpet", 150.00, false)]
        [TestCase("not valid", "73", "Carpet", 150.00, false)]

        public void OrderAddTest(string customerName, string state, string productType, decimal area, bool expectedResult)
        {
            int originalOrderNumber = 0;

            DateTime date = DateTime.Today;

            date = date.AddYears(1);

            Order order = new Order() { CustomerName = customerName, State = state, ProductType = productType, Area = area };

            order.OrderDate = date;

            OrderManager manager = OrderManagerFactory.Create();

            AddOrderResponse response = manager.OrderAdd(order.OrderDate, order, originalOrderNumber);

            var actual = response.Success;

            Assert.AreEqual(expectedResult, actual);

            RemoveOrderResponse response2 = manager.OrderRemove(order.OrderDate, originalOrderNumber, order);
        }

        [Test]
        [TestCase("valid", "OH", "Carpet", 150.00, 1, true)]

        public void OrderRemoveTest(string customerName, string state, string productType, decimal area, int orderNumber, bool expectedResult)
        {

            Order order = new Order() { CustomerName = customerName, State = state, ProductType = productType, Area = area };

            OrderManager manager = OrderManagerFactory.Create();

            RemoveOrderResponse response = manager.OrderRemove(order.OrderDate, order.OrderNumber, order);

            var actual = response.Success;

            Assert.AreEqual(expectedResult, actual);

            AddOrderResponse response3 = manager.OrderAdd(order.OrderDate, order, order.OrderNumber);
        }

        [Test]
        [TestCase("valid", "OH", "Carpet", 150.00, 1, true)]
        [TestCase("not valid", "OH", "Carpet", 10.00, 1, false)]

        public void OrderEditTest(string customerName, string state, string productType, decimal area, int orderNumber, bool expectedResult)
        {
            DateTime date = DateTime.Today;

            date = date.AddYears(1);


            Order order = new Order() { CustomerName = customerName, State = state, ProductType = productType, Area = area , OrderNumber = orderNumber};

            order.OrderDate = date;

            OrderManager manager = OrderManagerFactory.Create();

            EditOrderResponse response = manager.OrderEdit(order);

            var actual = response.Success;

            Assert.AreEqual(expectedResult, actual);
        }


    }
}
