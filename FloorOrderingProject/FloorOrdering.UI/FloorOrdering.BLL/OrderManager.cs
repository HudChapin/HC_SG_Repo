using FloorOrdering.Data.Data;
using FloorOrdering.Data.Responses;
using FloorOrdering.Models.Interfaces;
using FloorOrdering.Models.Models;
using System;
using System.Linq;

namespace FloorOrdering.BLL
{
    public class OrderManager
    {
        private IOrdersRepository _ordersRepository;

        public OrderManager(IOrdersRepository orderRepository)
        {
            _ordersRepository = orderRepository;
        }

        public DisplayOrderResponse OrderLookup(DateTime date)
        {
            DisplayOrderResponse response = new DisplayOrderResponse();

            response.Order = _ordersRepository.LoadOrder(date);

            if (response.Order.Count() == 0)
            {
                response.Success = false;
                response.Message = $"There were no orders on {date}.";
            }

            else
            {
                response.Date = date;
                response.Success = true;
            }

            return response;
        }

        public AddOrderResponse OrderAdd(DateTime date, Order order, int originalOrderNumber)
        {
            AddOrderResponse response = new AddOrderResponse();

            response.order = new Order();

            if (order.OrderDate < DateTime.Now)
            {
                response.Success = false;
                response.Message = "The date you entered was not in the future.";
                return response;
            }

            if (order.Area < 100.00M)
            {
                response.Success = false;
                response.Message = "The area you entered was not greater than 100 sq feet.";
                return response;
            }

            TaxesRepository taxRepo = new TaxesRepository();

            Taxes tax = null;

            tax = taxRepo.List().FirstOrDefault(t => t.StateAbbreviation.ToUpper() == order.State);

            if (tax == null)
            {
                response.Success = false;
                response.Message = "The state abbreviation you entered does not match to any state we do business in";
                return response;
            }

            ProductRepository productRepo = new ProductRepository();

            Product product = null;

            product = productRepo.List().FirstOrDefault(p => p.ProductType.ToUpper() == order.ProductType.ToUpper());

            if (product == null)
            {
                response.Success = false;
                response.Message = "The product type you entered does not match to any of the prodcuts that we offer.";
                return response;
            }

            bool result = order.CustomerName.All(c => Char.IsLetterOrDigit(c) || c == '.' || c == ',' || c == ' ');

            order.CustomerName = order.CustomerName.Trim();

            if (order.CustomerName == string.Empty)
            {
                response.Success = false;
                response.Message = "The customer name you entered cannot contain only spaces";
                return response;
            }

            if (result == false)
            {
                response.Success = false;
                response.Message = "The customer name you entered contained an invalid value";
                return response;
            }

            else
            {
                _ordersRepository.Add(order, originalOrderNumber);
                response.Success = true;
                return response;
            }

        }


        public RemoveOrderResponse OrderRemove(DateTime date, int orderNumber, Order order)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();

            _ordersRepository.Delete(date, orderNumber);
            response.Success = true;
            response.Message = "The order has been successfully removed. Press any key to continue";
            return response;


        }

        public EditOrderResponse OrderEdit(Order order)
        {
            EditOrderResponse response = new EditOrderResponse();

            int originalOrderNumber = order.OrderNumber;

            _ordersRepository.Delete(order.OrderDate, originalOrderNumber);

            AddOrderResponse addResponse = OrderAdd(order.OrderDate, order, originalOrderNumber);

            if (addResponse.Success)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }

            return response;



        }

    }

}


