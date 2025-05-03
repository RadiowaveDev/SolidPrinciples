using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool isVIP { get; set; }
        public List<Reservation> ReservationHistory { get; set; } = new List<Reservation>();

        public override string ToString()
        {
            return $"Customer: {FullName}, Phone: {PhoneNumber}, VIP: {isVIP}";
        }
    }
}
