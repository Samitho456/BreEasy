using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreEasy
{
    public class Weather
    {
        public int Id { get; set; }
        public double TemperatureCelsius { get; set; }
        public double Humidity { get; set; }

        // Tilføjet for at matche ekstern API-logik
        public double RainAmountMM { get; set; }

        public DateTime TimestampUtc { get; set; } // Retningsmæssigt mere korrekt navn

        public Weather(int id, double temperatureCelsius, double humidity, double rainAmount, DateTime timestamp)
        {
            Id = id;
            TemperatureCelsius = temperatureCelsius;
            Humidity = humidity;
            RainAmountMM = rainAmount;
            TimestampUtc = timestamp;
        }

        public Weather() { }
    }
}