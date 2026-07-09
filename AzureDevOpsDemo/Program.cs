using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Swagger/OpenAPI (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AzureDevOpsDemo API",
        Version = "v1",
        Description = "API for demoing employees endpoints"
    });
});

// Register the in-memory employee repository used by tests and app
builder.Services.AddSingleton<AzureDevOpsDemo.Repositories.IEmployeeRepository, AzureDevOpsDemo.Repositories.EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger JSON and UI in development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AzureDevOpsDemo API v1");
        c.RoutePrefix = "swagger"; // UI available at /swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
