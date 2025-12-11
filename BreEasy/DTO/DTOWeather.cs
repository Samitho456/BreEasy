using System.Collections.Generic;

// Vigtigt: Namespacet skal matche det, du bruger i dit using statement
namespace BreEasy.DTO
{
    // --- KLASSER TIL AT DESERIALISERE API SVARET (Input Model) ---

    // Matcher 'hourly' sektionen i Open-Meteo JSON
    public class Hourly
    {
        // Property navne skal matche JSON felter 1:1 (case-sensitive)
        public List<string> time { get; set; }
        public List<double> rain { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<int> weather_code { get; set; }
        public List<double> temperature_80m { get; set; }
    }

    // Hovedklasse for Open-Meteo svaret
    public class OpenMeteoResponse
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        // ... (andre metadata felter er valgfrie)

        public Hourly hourly { get; set; } // <--- Denne bruges i din controller!
    }

    // ---------------------------------------------------------------


    // --- KLASSER TIL DIT OUTPUT (Output Model, Valgfri) ---

    // DTO til at holde et tidspunkt og regnmængde (Bruges internt i din controller)
    public class RainTime
    {
        public string Time { get; set; }
        public double RainAmountMM { get; set; }
    }
}