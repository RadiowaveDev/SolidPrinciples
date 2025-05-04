using ReservaRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        List<Table> GetAvailableTables(DateTime date, int partySize);
    }
}
