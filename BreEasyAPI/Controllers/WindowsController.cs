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
            Console.WriteLine("test");

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
        public IActionResult Post(Window window)
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
    }
}
