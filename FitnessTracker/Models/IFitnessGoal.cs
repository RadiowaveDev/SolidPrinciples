using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public interface IFitnessGoal
    {
        bool EvaluateProgress(User user);
        string DisplayProgress(User user);
        string GetDescription();
        DateTime GetStartDate();
        DateTime? GetTargetDate();
        bool IsCompleted();
        void MarkAsCompleted();
    }
}
