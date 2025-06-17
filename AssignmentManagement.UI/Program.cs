using AssignmentManagement.Core;
using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AssignmentManagement.UI
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			var services = new ServiceCollection();
			services.AddSingleton<iAssignmentService, AssignmentService>();
			services.AddSingleton<iAssignmentFormatter, AssignmentFormatter>();
			services.AddSingleton<iLogger, ConsoleLogger>();
			services.AddSingleton<ConsoleUI>();

			var serviceProvider = services.BuildServiceProvider();
			var consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();

			consoleUI.Run();
		}
	}
}
