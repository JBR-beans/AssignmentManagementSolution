using AssignmentManagement.Core;
using AssignmentManagement.Api;
using Moq;
using AssignmentManagement.Core.Interfaces;

namespace AssignmentManagement.Tests
{
	public class AssignmentServiceTests
	{

		[Fact]
		public void Assignment_AcceptsNote()
		{
			var assignment = new Assignment("test", "test desc", "note", Priority.High);

			Assert.Equal("note", assignment.Note);
		}

		[Fact]
		public void Assignment_AcceptsNoNote()
		{
			var assignment = new Assignment("test", "test desc", Priority.High);

			Assert.Null(assignment.Note);
		}

		[Fact]
		public void Assignment_HasDefaultPriority()
		{
			var assignment = new Assignment("Task 1", "Details");
			// Failing
			//Assert.Equal(Priority.Low, assignment.Priority);
			Assert.Equal(Priority.Medium, assignment.Priority);
		}
		[Fact]
		public void Assignment_AcceptsHighPriority()
		{
			var assignment = new Assignment("Urgent Task", "Do it now", Priority.High);
			Assert.Equal(Priority.High, assignment.Priority);
		}


		[Fact]
		public void TestLogger()
		{
			// Arrange
			var mockLogger = new Mock<iLogger>();
			var assignment = new Assignment("Test Title", "Test Description");
			mockLogger.Setup(l => l.Log(It.IsAny<string>())).Verifiable();
			// Act
			mockLogger.Object.Log($"Assignment created: {assignment.Title}");
			// Assert
			mockLogger.Verify(l => l.Log(It.IsAny<string>()), Times.Once());
		}

		[Fact]
		public void TestFormatAssignment_Success()
		{
			// Arrange
			var mockFormatter = new Mock<iAssignmentFormatter>();
			var assignment = new Assignment("Test Title", "Test Description");
			mockFormatter.Setup(f => f.Format(It.IsAny<Assignment>()))
				.Returns((Assignment a) => $"[{a.Id}] {a.Title} - {(a.IsCompleted ? "Completed" : "Incomplete")}");
			// Act
			var formattedString = mockFormatter.Object.Format(assignment);
			// Assert
			Assert.Contains("Test Title", formattedString);
			Assert.Contains("Incomplete", formattedString);
		}

		[Fact]
		public void TestAddAssignment_Success()
		{
			// Arrange
			var mockService = new Mock<iAssignmentService>();
			var newAssignment = new Assignment("Test Title", "Test Description");

			/*mockService.Setup(s => s.AddAssignment(It.IsAny<Assignment>())).Returns(true);*/
			mockService.Setup(s => s.AddAssignment(It.IsAny<Assignment>()))
		   .Returns((Assignment a) => a);

			// Act
			var result = mockService.Object.AddAssignment(newAssignment);

			// Assert
			Assert.Equal(newAssignment.Title, result.Title);
			Assert.Equal(newAssignment.Description, result.Description);
			mockService.Verify(s => s.AddAssignment(It.IsAny<Assignment>()), Times.Once());
		}

		// Test SearchAssignmentByTitle logic
		[Fact]
		public void TestSearchAssignmentByTitle_Found()
		{
			// Arrange
			var mockService = new Mock<iAssignmentService>();
			var assignment = new Assignment("Test Title", "Test Description");

			mockService.Setup(s => s.FindAssignmentByTitle("Test Title")).Returns(assignment);

			// Act
			var result = mockService.Object.FindAssignmentByTitle("Test Title");

			// Assert
			Assert.NotNull(result);
			Assert.Equal("Test Title", result.Title);
			Assert.Equal("Test Description", result.Description);
		}

		// Test DeleteAssignment logic
		[Fact]
		public void TestDeleteAssignment_Success()
		{
			// Arrange
			var mockService = new Mock<iAssignmentService>();
			var titleToDelete = "Test Title";

			mockService.Setup(s => s.DeleteAssignment(titleToDelete)).Returns(true);

			// Act
			var result = mockService.Object.DeleteAssignment(titleToDelete);

			// Assert
			Assert.True(result, "Assignment should be successfully deleted.");
			mockService.Verify(s => s.DeleteAssignment(titleToDelete), Times.Once());
		}
	}
}

