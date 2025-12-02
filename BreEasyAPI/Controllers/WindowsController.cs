using BreEasy;
using BreEasy.EFDbContext;
using Microsoft.AspNetCore.Mvc;

namespace BreEasyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WindowsController : ControllerBase
    {
        // Local instance of the repository
        private readonly WindowsDbRepo _repo;

        // Constructor that accepts a WindowsDbRepo instance
        public WindowsController(WindowsDbRepo windowDbRepo)
        {
            _repo = windowDbRepo;
        }

        // Get all windows
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() // 1. Change return type to async Task<IActionResult>
        {
            // Use try-catch to handle potential errors
            try
            {
                // 2. Add 'await' here to extract the data from the Task
                IEnumerable<Window> windows = await _repo.GetAll();

                // Now you are sending the actual list, not the Task object
                return Ok(windows);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        // Add a new window
        [HttpPost]
        public IActionResult Post([FromBody] Window window)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the Add method on the repository
                _repo.Add(window);

                // Return 200 OK if successful
                return Ok(new { Id = window.Id });
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        // Get window by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the GetById method on the repository
                Window window = await _repo.GetById(id);

                // Return the window if found
                return Ok(window);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        // Get window by Location ID
        [HttpPost("location/{id}")]
        public async Task<IActionResult> GetByLocation(int id)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the GetByLocation method on the repository
                Window window = await _repo.GetByLocation(id);

                // Return the window if found
                return Ok(window);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        // Delete window by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the Remove method on the repository
                Window removedWindow = await _repo.Remove(id);

                // Return the removed window if found
                return Ok(removedWindow);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }

        // Update window by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Window window)
        {
            // Use try-catch to handle potential errors
            try
            {
                // Call the Update method on the repository
                Window updatedWindow = await _repo.Update(id, window);

                // Return the updated window if found
                return Ok(updatedWindow);
            }
            // If there's an error, return 404 Not Found
            catch
            {
                return NotFound();
            }
        }
    }
}
