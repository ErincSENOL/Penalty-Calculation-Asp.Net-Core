using PenaltyCalculation.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculation.Abstract
{
    public interface IPenaltyCalculation
    {
        dynamic CalculateDay(int penaltyDate);
        int GetBusinessDays(PenaltyInputModel penaltyInputModel);
    }
}
