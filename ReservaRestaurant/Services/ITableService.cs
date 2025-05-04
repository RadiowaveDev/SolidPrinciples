using ReservaRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Services
{
    public interface ITableService
    {
        List<Table> GetAvailableTables(DateTime date, int partySize);
        Table GetTableById(int id);
        List<Table> GetAllTables();
    }
}
