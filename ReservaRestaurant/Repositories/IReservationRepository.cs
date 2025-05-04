using ReservaRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Repositories
{
    public interface IReservationRepository: IRepository<Reservation>
    {
        List<Reservation> GetReservationsByDate(DateTime date);
        List<Reservation> GetReservationsByCustomer(int customerId);
    }
}
