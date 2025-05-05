using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ActivityType Type { get; set; }
        public decimal CaloriesPerMinute { get; set; }
    }
}
