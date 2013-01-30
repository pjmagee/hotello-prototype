namespace Hotello.Services.Wunderground.Weather.Models
{
    public class Date
    {
        public string Epoch { get; set; }
        public string Pretty { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Yday { get; set; }
        public int Hour { get; set; }
        public string Min { get; set; }
        public int Sec { get; set; }
        public string Isdst { get; set; }
        public string Monthname { get; set; }
        public string WeekdayShort { get; set; }
        public string Weekday { get; set; }
        public string Ampm { get; set; }
        public string TzShort { get; set; }
        public string TzLong { get; set; }
    }
}