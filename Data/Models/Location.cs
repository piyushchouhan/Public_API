namespace Data.Models
{
    public class Location
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Tz_Id { get; set; }
        public long Localtime_Epoch { get; set; }
        public string Localtime { get; set; }

    }
}
