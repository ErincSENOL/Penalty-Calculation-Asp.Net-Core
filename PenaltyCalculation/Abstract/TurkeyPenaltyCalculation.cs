using Microsoft.AspNetCore.Mvc.Rendering;
using PenaltyCalculation.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculation.Abstract
{
    public class TurkeyPenaltyCalculation : IPenaltyCalculation
    {
        private readonly int DayLimitOfPenalty = 15;
        private readonly decimal DayPriceOfPenalty = 5;


        public dynamic CalculateDay(int days)
        {
            decimal TotalPrice = days > DayLimitOfPenalty ? (days - DayLimitOfPenalty) * DayPriceOfPenalty : 0;
            return TotalPrice;
        }
        public int GetBusinessDays(PenaltyInputModel InputModel)
        {
            int BusinessDays = 0;
            var holidays = InputModel.Country.Holidays.Select(x => x.HolidayDate);
            var totalDays = InputModel.CheckInDate.Subtract(InputModel.CheckOutDate).TotalDays;
            for (var i = InputModel.CheckOutDate; i<= InputModel.CheckInDate; i= i.AddDays(1))
            {
                if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday && !holidays.Contains(i))
                {
                    BusinessDays++;
                }
            }
            return BusinessDays;
        }


        

    }
}
