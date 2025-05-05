using FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        List<Activity> _activities = new List<Activity>();
        private int _nextId = 1;

        public void Add(Activity entity)
        {
            entity.Id = _nextId++;
            _activities.Add(entity);
        }

        public void Delete(Activity entity)
        {
            _activities.Remove(entity);
        }

        public Activity getActivityByType(ActivityType activity)
        {
            return _activities.FirstOrDefault(a => a.Type == activity);
        }

        public List<Activity> GetAll()
        {
            return _activities;
        }

        public Activity GetById(int id)
        {
            return _activities.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Activity entity)
        {
            var index = _activities.FindIndex(x => x.Id == entity.Id);
            if (index < -1)
            {
                _activities[index] = entity;
            }
        }
    }
}
