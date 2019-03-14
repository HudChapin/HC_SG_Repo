using FloorOrdering.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.Models.Interfaces
{
    public interface IOrdersRepository
    {
        List<Order> LoadOrder(DateTime orderDate);
        Order Add(Order order, int originalOrderNumber);
        void Delete(DateTime orderDate, int orderNumber);
    }
}
