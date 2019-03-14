using FloorOrdering.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrdering.Data.Data
{
    public class TaxesRepository
    {

        public TaxesRepository()
        {
            
        }

        public List<Taxes> List()
        {
            List<Taxes> taxes = new List<Taxes>();

            using (StreamReader sr = new StreamReader(@"C:\Users\cohorts\repos\hud-chapin-individual-work\FloorOrderingProject\FloorOrdering.UI\FloorOrdering.Data\Taxes.txt")) 
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Taxes taxInfo = new Taxes();

                    string[] columns = line.Split(',');

                    taxInfo.StateAbbreviation = columns[0];
                    taxInfo.StateName = columns[1];
                    taxInfo.TaxRate = decimal.Parse(columns[2]);

                    taxes.Add(taxInfo);

                }
            }
            return taxes;
        }
    }
}

