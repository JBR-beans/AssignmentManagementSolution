using AssignmentManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Services
{
    public class AssignmentFormatter : iAssignmentFormatter
    {
		public string Format(Assignment assignment)
		{
			return $"[{assignment.Id}] {assignment.Priority} | {assignment.Title} - {(assignment.IsCompleted ? "Completed" : "Incomplete")}";
		}
	}
}
