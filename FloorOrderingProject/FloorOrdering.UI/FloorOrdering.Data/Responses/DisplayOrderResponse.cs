using FloorOrdering.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.Data.Responses
{
    public class DisplayOrderResponse : Response
    {
        public List<Order> Order { get; set; }
        public DateTime Date { get; set; }
        public string filepath { get; set; }
    }
}
