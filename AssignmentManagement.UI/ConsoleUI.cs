
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AssignmentManagement.Core;
using AssignmentManagement.Core.Interfaces;

namespace AssignmentManagement.UI
{
	public class ConsoleUI
	{

		private readonly iAssignmentFormatter _formatter;
		private readonly iAssignmentService _assignmentService;

		// Constructor to initialize the service
		public ConsoleUI(iAssignmentService assignmentService, iAssignmentFormatter formatter)
		{
			_assignmentService = assignmentService;
			_formatter = formatter;
		}

		private void ListOptions()
		{
			Console.WriteLine("\nAssignment Manager Menu:");
			Console.WriteLine("1. Add Assignment");
			Console.WriteLine("2. List All Assignments");
			Console.WriteLine("3. List Incomplete Assignments");
			Console.WriteLine("4. Mark Assignment as Complete");
			Console.WriteLine("5. Search Assignment by Title");
			Console.WriteLine("6. Update Assignment");
			Console.WriteLine("7. Delete Assignment");
			Console.WriteLine("0. Exit");
			Console.Write("Choose an option: ");
		}
		public void Run()
		{
			while (true)
			{
				ListOptions();

				var input = Console.ReadLine();

				if (!HandleInput(input))
				{
					Console.WriteLine("Goodbye!");
					return;
				}
			}
		}
		private bool HandleInput(string input)
		{
			switch (input)
			{
				case "1":
					AddAssignment();
					break;
				case "2":
					ListAllAssignments();
					break;
				case "3":
					ListIncompleteAssignments();
					break;
				case "4":
					MarkAssignmentComplete();
					break;
				case "5":
					SearchAssignmentByTitle(); 
					break;
				case "6":
					UpdateAssignment();
					break;
				case "7":
					DeleteAssignment();
					break;
				case "0":
					return false;
				default:
					Console.WriteLine("Invalid choice. Try again.");
					break;
			}
			return true;
		}

		private void AddAssignment()
		{
			Console.Write("Enter the assignment title: ");
			var title = Console.ReadLine();
			Console.Write("Enter the assignment description: ");
			var description = Console.ReadLine();
			var dueDate = Console.ReadLine();
			if (!DateTime.TryParse(dueDate, out var parsedDueDate))
			{
				Console.WriteLine("Invalid due date format.");
				return;
			}
			Console.Write("Enter the assignment priority: ");
			var priorityInput = Console.ReadLine();
			Enum.TryParse<Priority>(priorityInput, true, out Priority priority);
			Console.Write("Enter a note, or press Enter to skip: ");
			var note = Console.ReadLine();


			var assignment = new Assignment(title, description, parsedDueDate, note, priority);
			
			//var assignment = new Assignment(title, description, );
			if (_assignmentService.AddAssignment(assignment) != null)
			{
				Console.WriteLine("Assignment added successfully!");
			}
			else
			{
				Console.WriteLine("Assignment with this title already exists.");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private void ListAllAssignments()
		{
			var assignments = _assignmentService.ListAll();
			if (assignments.Count == 0)
			{
				Console.WriteLine("No assignments found.");
			}
			else
			{
				foreach (var assignment in assignments)
				{
					Console.WriteLine(_formatter.Format(assignment));
				}
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private void ListIncompleteAssignments()
		{
			var assignments = _assignmentService.ListIncomplete();
			if (assignments.Count == 0)
			{
				Console.WriteLine("No incomplete assignments found.");
			}
			else
			{
				foreach (var assignment in assignments)
				{
					Console.WriteLine(_formatter.Format(assignment));
				}
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private void MarkAssignmentComplete()
		{
			Console.Write("Enter the title of the assignment to mark complete: ");
			var title = Console.ReadLine();

			if (_assignmentService.MarkAssignmentComplete(title))
			{
				Console.WriteLine("Assignment marked as complete.");
			}
			else
			{
				Console.WriteLine("Assignment not found.");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private void SearchAssignmentByTitle()
		{
			Console.Write("Enter the title to search: ");
			var title = Console.ReadLine();

			var assignment = _assignmentService.FindAssignmentByTitle(title);

			if (assignment == null)
			{
				Console.WriteLine("Assignment not found.");
			}
			else
			{
				Console.WriteLine(_formatter.Format(assignment));
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private void UpdateAssignment()
		{
			Console.Write("Enter the title of the assignment to update: ");
			var oldTitle = Console.ReadLine();
			Console.Write("Enter the new title: ");
			var newTitle = Console.ReadLine();
			Console.Write("Enter the new description: ");
			Console.WriteLine("Enter a due date: ");
			var newDueDate = Console.ReadLine();
			if (!DateTime.TryParse(newDueDate, out var parsedDueDate))
			{
				Console.WriteLine("Invalid due date format.");
				return;
			}
			var newDescription = Console.ReadLine();
			Console.Write("Enter the assignment priority: ");
			var newPriority = Console.ReadLine();
			Enum.TryParse<Priority>(newPriority, true, out Priority priority);
			Console.Write("Enter a note, or press Enter to skip: ");
			var newNote = Console.ReadLine();

			if (_assignmentService.UpdateAssignment(oldTitle, newTitle, newDescription, parsedDueDate, newNote, priority))
			{
				Console.WriteLine("Assignment updated successfully.");
			}
			else
			{
				Console.WriteLine("Assignment not found or update failed.");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private void DeleteAssignment()
		{
			Console.Write("Enter the title of the assignment to delete: ");
			var title = Console.ReadLine();

			if (_assignmentService.DeleteAssignment(title))
			{
				Console.WriteLine("Assignment deleted successfully.");
			}
			else
			{
				Console.WriteLine("Assignment not found.");
			}

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}
}

