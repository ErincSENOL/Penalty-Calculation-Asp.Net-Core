using System;

namespace PenaltyCalculation.Models
{
    public class Holiday
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

    }
}
