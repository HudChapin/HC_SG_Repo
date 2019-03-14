using FloorOrdering.Data.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorOrdering.Data;

namespace FloorOrdering.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new OrderManager(new OrderTestRepository());
                case "Prod":
                    return new OrderManager(new OrderRepository());
                default:
                    return null;
                  
            }
        }
    }
}
