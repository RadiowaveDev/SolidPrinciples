using ReservaRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.Repositories
{
    public class TableRepository : ITableRepository
    {
        List<Table> _tables = new List<Table>();
        private int _nextId = 1;
        public TableRepository() 
        {
            // Initialize some tables
            _tables.Add(new Table { Id = _nextId++, Number = 1, Capacity = 2, IsAvailable = true, Location = "Window" });
            _tables.Add(new Table { Id = _nextId++, Number = 2, Capacity = 2, IsAvailable = true, Location = "Window" });
            _tables.Add(new Table { Id = _nextId++, Number = 3, Capacity = 4, IsAvailable = true, Location = "Inside" });
            _tables.Add(new Table { Id = _nextId++, Number = 4, Capacity = 4, IsAvailable = true, Location = "Inside" });
            _tables.Add(new Table { Id = _nextId++, Number = 5, Capacity = 6, IsAvailable = true, Location = "Patio" });
            _tables.Add(new Table { Id = _nextId++, Number = 6, Capacity = 8, IsAvailable = true, Location = "Patio" });
        }
        public void Add(Table entity)
        {
            entity.Id = _nextId++;
            _tables.Add(entity);
        }

        public void Delete(Table entity)
        {
            _tables.Remove(entity);
        }

        public List<Table> GetAll()
        {
            return _tables; 
        }

        public Table GetById(int id)
        {
            return _tables.FirstOrDefault(t => t.Id == id);
        }

        public void Update(Table entity)
        {
            var index = _tables.FindIndex(t => t.Id == entity.Id);
            if (index != -1)
            {
                _tables[index] = entity;
            }
        }

        public List<Table> GetAvailableTables(DateTime date, int partySize)
        {
            return _tables.Where(t => t.IsAvailable && t.Capacity >= partySize).ToList();
        }

        
    }
}
