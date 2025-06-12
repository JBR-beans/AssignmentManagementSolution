using AssignmentManagement.Core;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssignmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly iAssignmentService _service;
		public AssignmentsController(iAssignmentService service)
		{
			_service = service;
		}
		// GET: api/<AssignmentsController>
		[HttpGet]
        public IActionResult ListAll()
        {
			var assignment = _service.ListAll();
			return Ok(assignment);
		}
		[HttpGet("title")]
		public IActionResult FindByTitle(string title)
		{
			var assignment = _service.FindAssignmentByTitle(title);
			return assignment is null ? NotFound() : Ok(assignment);
		}

		[HttpPost]
		public IActionResult CreateAssignment([FromBody] Assignment assignment)
		{
			var Createdassignment = _service.AddAssignment(assignment);
			if (Createdassignment == null)
			{
				return BadRequest("An assignment with the same title already exists.");
			}
			return CreatedAtAction(nameof(FindByTitle), new { Title = Createdassignment.Title }, Createdassignment);
		}

		[HttpDelete("{title}")]
		public IActionResult Delete(string title)
		{
			var success = _service.DeleteAssignment(title);
			if (!success)
			{
				return NotFound($"Assignment '{title}' not found.");
			}

			return NoContent();
		}
	}
}
