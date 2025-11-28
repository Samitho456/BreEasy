namespace BreEasy
{
    public class Location
    {
        private string _locationName;
        private double _humidity;

        public int Id { get; set; }
        public string LocationName 
        {
            get { return _locationName; }

            set 
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Location name cannot be empty or whitespace.");
                }
                _locationName = value;
            }
        }

        public double Humidity 
        { 
            get { return _humidity; }
            set
            {
                if(value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Humidity must be between 0 and 100.");
                }
                _humidity = value;
            }
        }

        public Location() { }
        public Location(int id, string locationName)
        {
            Id = id;
            LocationName = locationName;
        }

        public override string ToString()
        {
            return $"Location Id: {Id}, Location Name: {LocationName}, Humidity: {Humidity}";
        }
    }
}
