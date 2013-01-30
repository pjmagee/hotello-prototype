namespace Hotello.Services.Wunderground.Weather.Models
{
    public class Forecastday
    {
        public int Period { get; set; }
        public string Icon { get; set; }
        public string IconUrl { get; set; }
        public string Title { get; set; }
        public string Fcttext { get; set; }
        public string FcttextMetric { get; set; }
        public string Pop { get; set; }
        public Date Date { get; set; }
        public High High { get; set; }
        public Low Low { get; set; }
        public string Conditions { get; set; }
        public string Skyicon { get; set; }
        public QpfAllday QpfAllday { get; set; }
        public QpfDay QpfDay { get; set; }
        public QpfNight QpfNight { get; set; }
        public SnowAllday SnowAllday { get; set; }
        public SnowDay SnowDay { get; set; }
        public SnowNight SnowNight { get; set; }
        public Maxwind Maxwind { get; set; }
        public Avewind Avewind { get; set; }
        public int Avehumidity { get; set; }
        public int Maxhumidity { get; set; }
        public int Minhumidity { get; set; }
    }
}