using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text;
using AssignmentManagement.Api;
using AssignmentManagement.Core;

namespace AssignmentManagement.ApiTests
{
    public class AssignmentManagementApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

		public AssignmentManagementApiTests(WebApplicationFactory<Program> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task Can_Create_And_Get_Assignment()
		{
			var assignmentJson = new StringContent(
			JsonSerializer.Serialize(new { Title = "Integration Test Assignment", Description = "Integration Test Note", Note = "note", Priority = Priority.High }),
			Encoding.UTF8, "application/json");
			var createResponse = await _client.PostAsync("/api/assignments", assignmentJson);
			createResponse.EnsureSuccessStatusCode();
			var getResponse = await _client.GetAsync("/api/assignments");
			getResponse.EnsureSuccessStatusCode();
			var json = await getResponse.Content.ReadAsStringAsync();
			var assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			Assert.Contains(assignments, a => a.Title == "Integration Test Assignment");
		}

		[Fact]
		public async Task Can_Get_All_Assignments()
		{
			var assignmentJson = new StringContent(JsonSerializer.Serialize(new { Title = "Get Test Assignment", Description = "Integration Test Note" }),Encoding.UTF8,"application/json");
			await _client.PostAsync("/api/assignments", assignmentJson);
			var getResponse = await _client.GetAsync("/api/assignments");
			getResponse.EnsureSuccessStatusCode();
			var json = await getResponse.Content.ReadAsStringAsync();
			var assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			Assert.NotEmpty(assignments);
			Assert.Contains(assignments, a => a.Title == "Get Test Assignment");
		}
		[Fact]
		public async Task Can_Get_Assignment_By_Title()
		{
			var createPayload = new StringContent(JsonSerializer.Serialize(new { Title = "GetByTitleTest", Description = "Test Description", Note = "Test Note", Priority = Priority.Low	}), Encoding.UTF8, "application/json");
			var createResponse = await _client.PostAsync("/api/assignments", createPayload);
			createResponse.EnsureSuccessStatusCode();
			var getResponse = await _client.GetAsync("/api/assignments/title?title=GetByTitleTest");
			getResponse.EnsureSuccessStatusCode();
			var json = await getResponse.Content.ReadAsStringAsync();
			var assignment = JsonSerializer.Deserialize<Assignment>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			Assert.NotNull(assignment);
			Assert.Equal("GetByTitleTest", assignment.Title);
			Assert.Equal("Test Description", assignment.Description);
			Assert.Equal("Test Note", assignment.Note);
			Assert.Equal(Priority.Low, assignment.Priority);
		}
		[Fact]
		public async Task Can_Delete_Assignment()
		{
			var assignmentJson = new StringContent(JsonSerializer.Serialize(new { Title = "Delete Test Assignment", Description = "To be deleted" }),Encoding.UTF8,"application/json");
			var createResponse = await _client.PostAsync("/api/assignments", assignmentJson);
			createResponse.EnsureSuccessStatusCode();
			var deleteResponse = await _client.DeleteAsync("/api/assignments/Delete Test Assignment");
			deleteResponse.EnsureSuccessStatusCode();
			var getResponse = await _client.GetAsync("/api/assignments");
			getResponse.EnsureSuccessStatusCode();
			var json = await getResponse.Content.ReadAsStringAsync();
			var assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			Assert.DoesNotContain(assignments, a => a.Title == "Delete Test Assignment");
		}

		[Fact]
		public async Task Can_Update_Assignment()
		{
			var assignmentJson = new StringContent(JsonSerializer.Serialize(new { Title = "Update Test Assignment", Description = "Old description", Note = "old note",Priority = Priority.Low }),Encoding.UTF8, "application/json");
			var createResponse = await _client.PostAsync("/api/assignments", assignmentJson);
			createResponse.EnsureSuccessStatusCode();
			var updatePayload = new StringContent(JsonSerializer.Serialize(new { Title = "Updated Assignment", Description = "New description", Note = "new note", Priority = Priority.High}),Encoding.UTF8,"application/json");
			var updateResponse = await _client.PutAsync("/api/assignments/Update Test Assignment", updatePayload);
			updateResponse.EnsureSuccessStatusCode();
			var getResponse = await _client.GetAsync("/api/assignments");
			getResponse.EnsureSuccessStatusCode();
			var json = await getResponse.Content.ReadAsStringAsync();
			var assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			Assert.Contains(assignments, a => a.Title == "Updated Assignment" && a.Description == "New description" && a.Note == "new note" && a.Priority == Priority.High);
			Assert.DoesNotContain(assignments, a => a.Title == "Update Test Assignment");
		}
	}
}
