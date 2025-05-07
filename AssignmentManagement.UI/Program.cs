using AssignmentManagement.Core;
using Microsoft.Extensions.DependencyInjection;

namespace AssignmentManagement.UI
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			// Step 1: Create the AssignmentService instance
			var assignmentService = new AssignmentService();

			// Step 2: Create the ConsoleUI instance, passing the AssignmentService instance to it
			var consoleUI = new ConsoleUI(assignmentService);

			// Step 3: Start the Console UI menu system
			consoleUI.ShowMenu();
		}
	}
}
