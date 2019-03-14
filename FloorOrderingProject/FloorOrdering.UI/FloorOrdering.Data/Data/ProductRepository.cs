using FloorOrdering.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.Data.Data
{
    public class ProductRepository
    {

        public ProductRepository()
        {

        }

        public List<Product> List()
        {
            List<Product> products  = new List<Product>();

            using (StreamReader sr = new StreamReader(@"C:\Users\cohorts\repos\hud-chapin-individual-work\FloorOrderingProject\FloorOrdering.UI\FloorOrdering.Data\Products.txt"))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Product product = new Product();

                    string[] columns = line.Split(',');

                    product.ProductType = columns[0];
                    product.CostPerSquareFoot = decimal.Parse(columns[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                    products.Add(product);
;                }
            }
            return products;
        }
    }
}
