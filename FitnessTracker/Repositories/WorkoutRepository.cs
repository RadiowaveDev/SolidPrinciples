using FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Repositories
{
    internal class WorkoutRepository : IWorkoutRepository
    {
        public List<WorkoutSession> _workouts = new List<WorkoutSession>();
        private int _nextId = 1;

        public void AddWorkout(WorkoutSession workout)
        {
            workout.Id = _nextId++;
            _workouts.Add(workout);
        }

        public List<WorkoutSession> GetWorkoutsByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public List<WorkoutSession> GetWorkoutsByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
