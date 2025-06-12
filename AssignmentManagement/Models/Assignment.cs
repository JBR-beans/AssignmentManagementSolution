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
		public bool IsCompleted { get; set; }
		public Priority Priority { get; set; }
		public string? Note { get; set; }


		public Assignment() { }
		public Assignment(string title, string description, Priority priority = Priority.Medium)
		{
			Validate(title, nameof(title));
			Validate(description, nameof(description));

			Title = title;
			Description = description;
			IsCompleted = false;
			Priority = priority;
		}
		public Assignment(string title, string description, string note, Priority priority = Priority.Medium)
		{
			Validate(title, nameof(title));
			Validate(description, nameof(description));

			Title = title;
			Description = description;
			IsCompleted = false;
			Priority = priority;
			Note = note;
		}

		public void Update(string newTitle, string newDescription, string newNote, Priority newPriority)
		{
			Validate(newTitle, nameof(newTitle));
			Validate(newDescription, nameof(newDescription));

			Title = newTitle;
			Description = newDescription;
			Priority = newPriority;
			Note = newNote;
		}

		public void MarkComplete()
		{
			IsCompleted = true;
		}

		private void Validate(string input, string fieldName)
		{
			if (string.IsNullOrWhiteSpace(input))
				throw new ArgumentException($"{fieldName} cannot be blank or whitespace.");
		}
	}
}
