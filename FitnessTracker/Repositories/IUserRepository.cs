using FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByGender(UserGender gender);
        User GetUserByAge(int age);
    }
}
