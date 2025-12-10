using BreEasy;
using Microsoft.AspNetCore.Mvc;

namespace BreEasyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        // Local instance of the repository
        private readonly LocationDbRepo _repo;

        // Constructor that accepts a LocationsDbRepo instance
        public LocationsController(LocationDbRepo locationDbRepo)
        {
            _repo = locationDbRepo;
        }

        // Get all locations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() // 1. Change return type to async Task<IActionResult>
        {
            // Use try-catch to handle potential errors
            try
            {
                // 2. Add 'await' here to extract the data from the Task
                IEnumerable<Location> locations = await _repo.GetAll();

                // Now you are sending the actual list, not the Task object
                return Ok(locations);
            }
            // If there's an error, return 404 Not Found
            catch (ArgumentException ex)
            {
                {
                    Console.WriteLine(ex.Message);
                    return NotFound();

                }
            }
        }

        // Add a new Location
        [HttpPost]
        public IActionResult Post([FromBody] Location location)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the Add method on the repository
                _repo.Add(location);

                // Return 200 OK if successful
                return Ok(new { Id = location.Id });
        }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        // Get Location by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the GetById method on the repository
                Location location = await _repo.GetById(id);

                // Return the location if found
                return Ok(location);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        // Delete location by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the Remove method on the repository
                Location removedLocation = await _repo.Remove(id);

                // Return the removed location if found
                return Ok(removedLocation);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        // Update location by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Location location)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the Update method on the repository
                Location updatedLocation = await _repo.Update(id, location);

                // Return the updated location if found
                return Ok(updatedLocation);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("humidity/{id}")]
        public async Task<IActionResult> UpdateHumidity(int id, double humidity)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the UpdateHumidity method on the repository
                Location updatedLocation = await _repo.UpdateHumidity(id, humidity);
                // Return the updated location if found
                return Ok(updatedLocation);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }
    }
}

