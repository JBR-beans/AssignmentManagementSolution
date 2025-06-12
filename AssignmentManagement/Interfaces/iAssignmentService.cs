using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core
{
    public interface iAssignmentService
    {
		public Assignment AddAssignment(Assignment assignment);

		public List<Assignment> ListAll();

        public List<Assignment> ListIncomplete();


        public Assignment FindAssignmentByTitle(string title);


        public bool MarkAssignmentComplete(string title);

        public bool DeleteAssignment(string title);


        public bool UpdateAssignment(string oldTitle, string newTitle, string newDescription, string newNote, Priority newPriority);
        Assignment? FindByTitle(string title);
    }
}
