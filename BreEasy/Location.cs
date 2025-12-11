namespace BreEasy
{
    public class Location
    {
        private string _locationName;
        private double _humidity;
        private double _temperature;
        private double _maxTemperature;
        private double _maxHumidity;

        // id of the location
        public int Id { get; set; }

        // name of the location
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

        // humidity of the location
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

        // maximum humidity of the location before the window closes
        public double MaxHumidity
        {
            get { return _maxHumidity; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Max Humidity must be between 0 and 100.");
                }
                _maxHumidity = value;
            }
        }

        // temperature of the location
        public double Temperature 
        {   
            get
            {
                return _temperature;
            }
            set 
            { 
                if (value < -50 || value > 60)
                {
                    throw new ArgumentOutOfRangeException("Temperature must be between -50 and 60 degrees Celsius.");
                }
                _temperature = value;
            }
        }

        // maximum temperature of the location before the window closes
        public double MaxTemperature
        {
            get { return _maxTemperature; }
            set
            {
                if (value < -50 || value > 60)
                {
                    throw new ArgumentOutOfRangeException("Max Temperature must be between -50 and 60 degrees Celsius.");
                }
                _maxTemperature = value;
            }
        }


        // default constructor
        public Location() { }

        // parameterized constructor
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
