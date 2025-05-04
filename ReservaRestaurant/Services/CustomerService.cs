using ReservaRestaurant.Models;
using ReservaRestaurant.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IReservationRepository _reservationRepository;

        public CustomerService(
            ICustomerRepository customerRepository,
            IReservationRepository reservationRepository)
        {
            this._customerRepository = customerRepository;
            this._reservationRepository = reservationRepository;
        }

        public Customer CreateCustomer(string name, string phone, string email)
        {
            var customer = _customerRepository.GetByEmail(email);

            if (customer == null)
            {
                customer = new Customer
                {
                    FullName = name,
                    Email = email,
                    PhoneNumber = phone
                };
                _customerRepository.Add(customer);
            }
            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _customerRepository.GetByEmail(email);
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public List<Reservation> GetCustomerReservations(int customerId)
        {
            return _reservationRepository.GetReservationsByCustomer(customerId);
        }

        public List<Customer> GetCustomers()
        {
            return _customerRepository.GetAll();
        }
    }
}
