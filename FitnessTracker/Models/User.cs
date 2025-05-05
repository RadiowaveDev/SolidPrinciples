using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Height  { get; set; }
        public UserGender Gender { get; set; }
        public List<WeightRecord> _weightHistory { get; private set; }
        public List<IFitnessGoal> _goals { get; private set; }

        public int GetAge()
        {
            return DateTime.Today.Year - DateOfBirth.Year -
            (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
        }

        public decimal GetCurrentWeight()
        {
            return _weightHistory.Count > 0 ? _weightHistory.OrderByDescending(p => p.Date).First().Weight : 0;
        }
    }
}
