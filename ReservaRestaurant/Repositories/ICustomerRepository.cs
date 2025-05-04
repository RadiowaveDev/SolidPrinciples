using ReservaRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByPhoneNumber(string phoneNumber);
        Customer GetByEmail(string email);
    }
}
