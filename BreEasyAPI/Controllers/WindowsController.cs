using BreEasy;
using BreEasy.EFDbContext;
using Microsoft.AspNetCore.Mvc;

namespace BreEasyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WindowsController : ControllerBase
    {
        private readonly WindowsDbRepo _repo;
        public WindowsController(WindowsDbRepo windowDbRepo)
        {
            _repo = windowDbRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() // 1. Change return type to async Task<IActionResult>
        {
            try
            {
                // 2. Add 'await' here to extract the data from the Task
                IEnumerable<Window> windows = await _repo.GetAll();

                // Now you are sending the actual list, not the Task object
                return Ok(windows);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Window window)
        {
            try
            {
                _repo.Add(window);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Window window = await _repo.GetById(id);
                return Ok(window);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("location/{id}")]
        public async Task<IActionResult> GetByLocation(int id)
        {
            try
            {
                Window window = await _repo.GetByLocation(id);
                return Ok(window);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Window removedWindow = await _repo.Remove(id);
                return Ok(removedWindow);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Window window)
        {
            try
            {
                Window updatedWindow = await _repo.Update(id, window);
                return Ok(updatedWindow);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
