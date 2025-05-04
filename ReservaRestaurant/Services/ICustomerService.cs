using ReservaRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Services
{
    public interface ICustomerService
    {
        Customer CreateCustomer(string name, string phone, string email);
        Customer GetCustomerById(int id);
        List<Customer> GetCustomers();
        Customer GetCustomerByEmail(string email);
        List<Reservation> GetCustomerReservations(int customerId);
    }
}
