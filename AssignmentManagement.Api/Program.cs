
using AssignmentManagement.Core;
using AssignmentManagement.Core.Interfaces;
using AssignmentManagement.Core.Services;

namespace AssignmentManagement.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.


			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();
			builder.Services.AddSingleton<iAssignmentFormatter, AssignmentFormatter>();
			builder.Services.AddSingleton<iLogger, ConsoleLogger>();
			builder.Services.AddSingleton<iAssignmentService, AssignmentService>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}


			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
