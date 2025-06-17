using Microsoft.VisualBasic;
using System;

namespace AssignmentManagement.Core
{

	public enum Priority
	{
		Low,
		Medium,
		High
	}

	public class Assignment
	{
		public Guid Id { get; } = Guid.NewGuid();
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime DueDate { get; private set; }
		public bool IsCompleted { get; set; }
		public Priority Priority { get; set; }
		public string? Note { get; set; }


		public Assignment() { }
		public Assignment(string title, string description, DateTime dueDate, Priority priority = Priority.Medium)
		{
			Validate(title, nameof(title));
			Validate(description, nameof(description));

			Title = title;
			Description = description;
			IsCompleted = false;
			Priority = priority;
			DueDate = dueDate;
		}
		public Assignment(string title, string description, DateTime dueDate, string note, Priority priority = Priority.Medium)
		{
			Validate(title, nameof(title));
			Validate(description, nameof(description));

			Title = title;
			Description = description;
			IsCompleted = false;
			Priority = priority;
			Note = note;
			DueDate = dueDate;
		}

		public void Update(string newTitle, string newDescription, DateTime newDueDate, string newNote, Priority newPriority)
		{
			Validate(newTitle, nameof(newTitle));
			Validate(newDescription, nameof(newDescription));

			Title = newTitle;
			Description = newDescription;
			Priority = newPriority;
			Note = newNote;
			DueDate = newDueDate;
		}

		public void MarkComplete()
		{
			IsCompleted = true;
		}
		public void MarkIncomplete()
		{
			IsCompleted = false;
		}

		private void Validate(string input, string fieldName)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentException($"{fieldName} cannot be blank or whitespace.");
			}
		}
	}
}
