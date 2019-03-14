using FloorOrdering.Models.Models;
using FloorOrdering.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.Data.Data
{
    public class OrderRepository : IOrdersRepository
    {

        public List<Order> LoadOrder(DateTime dateTime)
        {
            string filepath = string.Format(@"C:\Users\cohorts\repos\hud-chapin-individual-work\FloorOrderingProject\FloorOrdering.UI\FloorOrdering.Data\Orders_{0:MMddyyyy}.txt", dateTime);

            List<Order> orders = new List<Order>();

            if (File.Exists(filepath))
            {
               

                using (StreamReader sr = new StreamReader(filepath))
                {
                    sr.ReadLine();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Order order = new Order();

                        string[] columns = line.Split('~');

                        order.OrderNumber = int.Parse(columns[0]);
                        order.CustomerName = columns[1];
                        order.State = columns[2];
                        order.TaxRate = decimal.Parse(columns[3]);
                        order.ProductType = columns[4];
                        order.Area = decimal.Parse(columns[5]);
                        order.CostPerSquareFoot = decimal.Parse(columns[6]);
                        order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                        order.MaterialCost = decimal.Parse(columns[8]);
                        order.LaborCost = decimal.Parse(columns[9]);
                        order.Tax = decimal.Parse(columns[10]);
                        order.Total = decimal.Parse(columns[11]);

                        orders.Add(order);
                    }
                }

            }
            return orders;
        }



        private int nextId(DateTime dateTime, int originalOrderNumber)
        {
            if (originalOrderNumber == 0)
            {
                int id = 0;

                List<Order> orders = LoadOrder(dateTime);

                foreach (Order order in orders)
                {
                    if (order.OrderNumber > id)
                    {
                        id = order.OrderNumber;
                    }
                }
                id++;
                return id;
            }

            else
            {
                int id = originalOrderNumber;
                return id;
            }
        }    


        public Order Add(Order order, int originalOrderNumber)
        {
            order.OrderNumber = nextId(order.OrderDate, originalOrderNumber);

            List<Order> orders = LoadOrder(order.OrderDate);

            orders.Add(order);

            CreateOrderFile(order.OrderDate, orders);

            return order;

        }

        public void Delete(DateTime orderDate, int orderNumber)
        {
            List<Order> orders = LoadOrder(orderDate);

            List<Order> newOrderList = new List<Order>();

            foreach (Order order in orders)
            {
                if (orderNumber != order.OrderNumber)
                {
                    newOrderList.Add(order);
                }
            }

            CreateOrderFile(orderDate, newOrderList);
        }

        private string CreateCsvForOrder(Order order)
        {
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}", order.OrderNumber,
                    order.CustomerName, order.State, order.TaxRate, order.ProductType, order.Area, order.CostPerSquareFoot,
                    order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);
        }
        private void CreateOrderFile(DateTime dateTime, List<Order> orders)
        {
            if (orders == null)
            {
                return;
            }
            string filepath = string.Format(@"C:\Users\cohorts\repos\hud-chapin-individual-work\FloorOrderingProject\FloorOrdering.UI\FloorOrdering.Data\Orders_{0:MMddyyyy}.txt", dateTime);

            using (StreamWriter sr = new StreamWriter(filepath))
            {
                sr.WriteLine("Order ID" + "~" + "Order Date" + "~" +  "Cust Name" + "~" + "State" + "~" + "Prod Type" + "~" + "Material Cost" + "~" + "Labor Cost" + "~" + "Tax" + "~" + "Total");
                foreach(var order in orders)
                {
                    sr.WriteLine(CreateCsvForOrder(order));
                }
            }
        }
    }
}
