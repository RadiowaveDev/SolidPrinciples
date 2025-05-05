using FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Repositories
{
    public interface IActivityRepository : IRepository<Activity>
    {
        Activity getActivityByType(ActivityType activity);
    }
}
