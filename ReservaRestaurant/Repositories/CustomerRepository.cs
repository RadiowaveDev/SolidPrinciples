using ReservaRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        List<Customer> _customers = new List<Customer>();
        private int _nextId = 1;

        public void Add(Customer entity)
        {
            entity.Id = _nextId++; //incremental para el valor de Id
            _customers.Add(entity);
        }

        public void Delete(Customer entity)
        {
            _customers.Remove(entity);
        }

        public List<Customer> GetAll()
        {
            return _customers;
        }

        public void Update(Customer entity)
        {
            var index = _customers.FindIndex(x => x.Id == entity.Id);
            if(index < -1)
            {
                _customers[index] = entity;
            }
        }
        public Customer GetById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id ==id);
        }
        
        public Customer GetByEmail(string email)
        {
            return _customers.FirstOrDefault(c => c.Email == email);
        }
        public Customer GetByPhoneNumber(string phoneNumber)
        {
            return _customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }

        
    }
}
