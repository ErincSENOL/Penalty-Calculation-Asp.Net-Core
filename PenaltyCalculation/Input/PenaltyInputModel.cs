using PenaltyCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculation.Input
{
    public class PenaltyInputModel
    {
        public DateTime CheckOutDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public Country Country { get; set; }
        public List<Holiday> Holidays { get; set; }
    }
}
