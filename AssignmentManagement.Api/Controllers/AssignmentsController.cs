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
		[HttpPut("{title}")]
		public IActionResult Update(string title, [FromBody] Assignment updatedAssignment)
		{
			if (updatedAssignment == null)
			{
				return BadRequest("Updated assignment data is required.");
			}

			var success = _service.UpdateAssignment(
				title,
				updatedAssignment.Title,
				updatedAssignment.Description,
				updatedAssignment.Note,
				updatedAssignment.Priority
			);

			if (!success)
			{
				return NotFound($"Assignment '{title}' not found or update failed.");
			}

			return NoContent();
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
