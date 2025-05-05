using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class WorkoutSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Activity Activity { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; } // in minutes
        public decimal Intensity { get; set; } // value between 0 and 1
        public string Notes { get; set; }
    }
}
