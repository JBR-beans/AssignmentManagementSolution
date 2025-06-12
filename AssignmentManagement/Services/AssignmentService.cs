using AssignmentManagement.Core.Interfaces;

namespace AssignmentManagement.Core
{
	public class AssignmentService : iAssignmentService
	{

		private readonly iAssignmentFormatter _assignmentformatter;
		private readonly iLogger _logger;
		private readonly List<Assignment> _assignments = new();
		
		public AssignmentService(iAssignmentFormatter assignmentformatter, iLogger logger)
		{
			_assignmentformatter = assignmentformatter;
			_logger = logger;
		}
		

		public Assignment? FindByTitle(string title)
		{
			return _assignments.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
		}
		public Assignment AddAssignment(Assignment assignment)
		{
			if (_assignments.Any(a => a.Title.Equals(assignment.Title, StringComparison.OrdinalIgnoreCase)))
			{
				return null; // Duplicate title exists
			}

			_assignments.Add(assignment);
			return assignment;
		}

		public List<Assignment> ListAll()
		{
			return new List<Assignment>(_assignments);
		}

		public List<Assignment> ListIncomplete()
		{
			return _assignments.Where(a => !a.IsCompleted).ToList();
		}

		public Assignment FindAssignmentByTitle(string title)
		{
			return _assignments
				.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
		}

		public bool MarkAssignmentComplete(string title)
		{
			var assignment = FindAssignmentByTitle(title);
			if (assignment == null)
				return false; // Assignment not found
			assignment.MarkComplete();
			return true; // Assignment marked as complete
		}

		public bool DeleteAssignment(string title)
		{
			var assignment = FindAssignmentByTitle(title);
			if (assignment == null)
				return false; // Assignment not found
			_assignments.Remove(assignment);
			return true; // Assignment deleted successfully

		}

		public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription, string newNote, Priority newPriority)
		{
			var assignment = FindAssignmentByTitle(oldTitle);
			if (assignment == null)
				return false; // Assignment not found
			try
			{
				assignment.Update(newTitle, newDescription, newNote, newPriority);
				return true; // Assignment updated successfully
			}
			catch (ArgumentException)
			{
				return false; // Update failed due to validation error
			}
		}
	}
}
