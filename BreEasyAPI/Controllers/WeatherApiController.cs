using BreEasy;
using BreEasy.DTO;
using BreEasy.EFDbContext;
using Microsoft.AspNetCore.Mvc;
using System; // Tilføjet for at sikre, at 'Exception' er tilgængelig
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BreEasyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherApiController : ControllerBase
    {
        private readonly WindowsDbRepo _repo;
        private readonly HttpClient _httpClient;

        // **ÆNDRING 1: Rettet URL-syntaks (fjernet overflødigt komma)**
        private readonly string _url =
            "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,rain,weather_code,temperature_80m";

        public WeatherApiController(IHttpClientFactory httpClientFactory, WindowsDbRepo windowDbRepo)
        {
            _httpClient = httpClientFactory.CreateClient();
            _repo = windowDbRepo; // Sørg for at tildele repo'en
        }

        // **ÆNDRING 2: Ny [HttpGet] for at undgå 404 på base-ruten /api/WeatherApi**
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                Status = "API er aktiv. Vejrdatabehandling kører.",
                RainForecastEndpoint = "/api/WeatherApi/next-rain-time"
            });
        }

        [HttpGet("next-rain-time")]
        public async Task<IActionResult> RainCloseWindow()
        {
            // Vi bruger UTC Time fra Open-Meteo, og derfor skal vi bruge DateTimeOffset.UtcNow 
            // for at sikre, at vi sammenligner rigtigt.
            var currentTime = DateTimeOffset.UtcNow;

            // Formattet, som Open-Meteo bruger (f.eks. "2025-12-09T01:00")
            const string TimeFormat = "yyyy-MM-ddTHH:mm";

            try
            {
                var response = await _httpClient.GetAsync(_url);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Failed to call external weather API");
                }

                var json = await response.Content.ReadAsStringAsync();

                // Deserialiser (OpenMeteoResponse DTO'en skal være opdateret til at matche JSON'en)
                var openMeteoData = JsonSerializer.Deserialize<OpenMeteoResponse>(json);

                if (openMeteoData?.hourly?.time == null || openMeteoData.hourly.rain == null)
                {
                    return StatusCode(500, "Failed to parse expected weather data structure.");
                }

                // 1. Find det første tidspunkt med regn
                // Linq: Vi itererer over listerne ved at bruge indexet 'i'
                for (int i = 0; i < openMeteoData.hourly.time.Count; i++)
                {
                    // Tjek om der er regn (RainAmountMM > 0.00)
                    if (openMeteoData.hourly.rain[i] > 0.00)
                    {
                        // Her har vi fundet den første time med regn
                        var nextRainTimeIso = openMeteoData.hourly.time[i];
                        var rainAmount = openMeteoData.hourly.rain[i];

                        // 2. Pars tidspunktet og beregn forskellen
                        // DateTimeOffset bruges, da Open-Meteo default returnerer UTC (timezone: "GMT", utc_offset_seconds: 0)
                        if (DateTimeOffset.TryParseExact(nextRainTimeIso, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var nextRainTimeUtc))
                        {
                            // Sikrer, at vi kun tjekker fremtidige tidspunkter
                            if (nextRainTimeUtc > currentTime)
                            {
                                var timeUntilRain = nextRainTimeUtc - currentTime;
                                var minutesUntilRain = (int)timeUntilRain.TotalMinutes;

                                // 3. Returner det præcise resultat
                                return Ok(new
                                {
                                    Message = $"Regn forventes at starte klokken {nextRainTimeUtc:HH:mm} UTC (om ca. {minutesUntilRain} minutter). Luk vinduet!",
                                    NextRainTimeUtc = nextRainTimeUtc,
                                    TimeUntilRainMinutes = minutesUntilRain,
                                    RainAmountMM = rainAmount
                                });
                            }
                        }
                    }
                }

                // Hvis loopet færdiggøres uden at finde regn
                return Ok(new
                {
                    Message = "Ingen regn forventes i det nuværende forecast.",
                    NextRainTimeUtc = (DateTimeOffset?)null,
                    TimeUntilRainMinutes = 99999
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling weather API: {ex.Message}");
            }
        }
    }
}