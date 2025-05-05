using FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        List<User> _users = new List<User>();
        private int _nextId = 1;

        public void Add(User entity)
        {
            entity.Id = _nextId++;
            _users.Add(entity);
        }

        public void Delete(User entity)
        {
            _users.Remove(entity);
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public User GetUserByAge(int age)
        {
            return _users.FirstOrDefault(u => u.GetAge() == age);
        }

        public User GetUserByGender(UserGender gender)
        {
            return _users.FirstOrDefault(u=> u.Gender == gender);
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void Update(User entity)
        {
            var index = _users.FindIndex(x => x.Id == entity.Id);
            if (index < -1)
            {
                _users[index] = entity;
            }
        }
    }
}
