using WebApiSseDemo.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<StreamSyncProgress>(_ => SyncStream.StreamProgressAsync);
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	// Generates the OpenAPI JSON document at /openapi/v1.json
	app.MapOpenApi();

	// Maps the Scalar UI at /scalar/v1
	app.MapScalarApiReference();

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
