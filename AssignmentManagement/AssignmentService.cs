namespace AssignmentManagement.Core
{
	public class AssignmentService : IAssignmentService
	{
		private readonly List<Assignment> _assignments = new();

		public Assignment? FindByTitle(string title)
		{
			return _assignments.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
		}
		public bool AddAssignment(Assignment assignment)
		{
			if (_assignments.Any(a => a.Title.Equals(assignment.Title, StringComparison.OrdinalIgnoreCase)))
			{
				return false; // Duplicate title exists
			}

			_assignments.Add(assignment);
			return true;
		}

		public List<Assignment> ListAll()
		{
			return new List<Assignment>(_assignments);
		}

		public List<Assignment> ListIncomplete()
		{
			return _assignments.Where(a => !a.IsCompleted).ToList();
		}

		// TODO: Implement method to find an assignment by title
		public Assignment FindAssignmentByTitle(string title)
		{
			return _assignments
				.FirstOrDefault(a => a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
		}

		// TODO: Implement method to mark an assignment complete
		public bool MarkAssignmentComplete(string title)
		{
			var assignment = FindAssignmentByTitle(title);
			if (assignment == null)
				return false; // Assignment not found
			assignment.MarkComplete();
			return true; // Assignment marked as complete
		}

		// TODO: Implement method to delete an assignment by title
		public bool DeleteAssignment(string title)
		{
			var assignment = FindAssignmentByTitle(title);
			if (assignment == null)
				return false; // Assignment not found
			_assignments.Remove(assignment);
			return true; // Assignment deleted successfully

		}

		// TODO: Implement method to update an assignment (title and description)
		public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription)
		{
			var assignment = FindAssignmentByTitle(oldTitle);
			if (assignment == null)
				return false; // Assignment not found
			try
			{
				assignment.Update(newTitle, newDescription);
				return true; // Assignment updated successfully
			}
			catch (ArgumentException)
			{
				return false; // Update failed due to validation error
			}
		}
	}
}
