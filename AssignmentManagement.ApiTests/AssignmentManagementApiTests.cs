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
			// create
			var assignmentJson = new StringContent(
			JsonSerializer.Serialize(new { Title = "Integration Test Assignment", Description = "Integration Test Note", Note = "note", Priority = Priority.High }),
			Encoding.UTF8, "application/json");

			var createResponse = await _client.PostAsync("/api/assignments", assignmentJson);
			createResponse.EnsureSuccessStatusCode();

			// get
			var getResponse = await _client.GetAsync("/api/assignments");
			getResponse.EnsureSuccessStatusCode();

			var json = await getResponse.Content.ReadAsStringAsync();
			var assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			// assert exists
			Assert.Contains(assignments, a => a.Title == "Integration Test Assignment");
		}

		[Fact]
		public async Task Can_Get_All_Assignments()
		{
			// arrange
			var assignmentJson = new StringContent(
				JsonSerializer.Serialize(new { Title = "Test Assignment", Description = "Integration Test Note" }),
				Encoding.UTF8, "application/json");

			await _client.PostAsync("/api/assignments", assignmentJson);

			// act
			var getResponse = await _client.GetAsync("/api/assignments");
			getResponse.EnsureSuccessStatusCode();

			var json = await getResponse.Content.ReadAsStringAsync();
			var assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			// assert
			Assert.NotEmpty(assignments);

			Assert.Contains(assignments, a => a.Title == "Test Assignment");
		}

	}
}
