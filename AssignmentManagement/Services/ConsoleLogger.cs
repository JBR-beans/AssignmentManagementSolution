﻿using AssignmentManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagement.Core.Services
{
    public class ConsoleLogger : iLogger
    {
		public void Log(string message)
		{
			Console.WriteLine($"[LOG] {message}");
		}
	}
}
