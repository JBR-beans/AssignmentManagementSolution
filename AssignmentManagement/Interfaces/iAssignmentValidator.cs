using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Interfaces
{
    public interface iAssignmentValidator
    {
        string ValidateString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
				throw new ArgumentException("Input cannot be null or empty.");
			}
            else
            {
				return input;
			}
		}
    }
}
