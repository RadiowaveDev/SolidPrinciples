using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public string Location { get; set; }    


        public override string ToString()
        {
            return $"Table #{Number}: Capacity: {Capacity}, Location: {Location}, Available: {IsAvailable} ";
        }
    }
}
