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
		
		public Assignment FindAssignmentByTitle(string title)
		{
			Assignment _assignment = _assignments.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
			if (_assignment == null)
			{
				_logger.Log($"{_assignment.Title} not found.");
				return null;
			}

			_logger.Log($"{_assignment.Title} found.");
			return _assignment;
		}
		public Assignment AddAssignment(Assignment assignment)
		{
			if (_assignments.Any(a => a.Title.Equals(assignment.Title, StringComparison.OrdinalIgnoreCase)))
			{
				_logger.Log($"Assignment {assignment.Title} already exists.");
				return null; 
			}
			
			_assignments.Add(assignment);
			_logger.Log($"Assignment {assignment.Title} added successfully.");
			return assignment;
		}

		public List<Assignment> ListAll()
		{
			_logger.Log($"{_assignments.Count} assignments found.");
			return new List<Assignment>(_assignments);
		}

		public List<Assignment> ListIncomplete()
		{
			List<Assignment> _incomplete = _assignments.Where(a => !a.IsCompleted).ToList();
			_logger.Log($"{_incomplete.Count} incomplete assignments found.");
			return _incomplete;
		}

		

		public bool MarkAssignmentComplete(string title)
		{
			var assignment = FindAssignmentByTitle(title);
			if (assignment == null)
			{
				_logger.Log($"{assignment.Title} not found.");
				return false; 
			}
			
			assignment.MarkComplete();
			_logger.Log($"{assignment.Title} marked complete.");
			return true; 
		}

		public bool DeleteAssignment(string title)
		{
			var assignment = FindAssignmentByTitle(title);
			if (assignment == null)
			{
				_logger.Log($"{assignment.Title} not found.");
				return false;
			}
				
			_assignments.Remove(assignment);
			_logger.Log($"{assignment.Title} removed.");
			return true;

		}

		public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription, string newNote, Priority newPriority)
		{
			var assignment = FindAssignmentByTitle(oldTitle);
			if (assignment == null)
			{
				_logger.Log($"{assignment.Title} not found.");
				return false;
			}
			
			try
			{
				assignment.Update(newTitle, newDescription, newNote, newPriority);
				_logger.Log($"{assignment.Title} updated.");
				return true; 
			}
			catch (ArgumentException e)
			{
				_logger.Log($"{assignment.Title} failed to update. | " + e.Message);
				return false; 
			}
		}
	}
}
