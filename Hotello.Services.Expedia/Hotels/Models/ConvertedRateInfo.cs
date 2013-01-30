using System.Collections.Generic;

namespace Hotello.Services.Expedia.Hotels.Models
{
    public class ConvertedRateInfo
    {
        public decimal Total { get; set; }
        public decimal SurchargeTotal { get; set; }
        public decimal NightlyRateTotal { get; set; }
        public decimal AverageBaseRate { get; set; }
        public decimal MaxNightlyRate { get; set; }
        public string CurrencyCode { get; set; }
        public List<NightlyRate> NightlyRatesPerRoom { get; set; }
        public List<Surcharge> Surcharges { get; set; }
        public string CommissionableUsdTotal;
        public string AverageRate;
    }
}