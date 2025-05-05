using FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Repositories
{
    public interface IWorkoutRepository
    {
        void AddWorkout(WorkoutSession workout);
        List<WorkoutSession> GetWorkoutsByUserId(int userId);
        List<WorkoutSession> GetWorkoutsByDateRange(int userId, DateTime startDate, DateTime endDate);
    }
}
