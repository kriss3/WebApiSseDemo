using WebApiSseDemo.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
	p.WithOrigins("http://localhost:54614")
	 .AllowAnyHeader()
	 .AllowAnyMethod()));

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


app.UseCors();
app.MapControllers();
app.Run();
