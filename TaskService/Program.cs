using TaskService.Application.Interfaces;
using TaskService.Infrastructure.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container  
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskService API", Version = "v1" });
});

// Dependency Injection  
builder.Services.AddSingleton<ITaskService, TaskServices>();

var app = builder.Build();

// Configure the HTTP request pipeline  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskService API v1"));
}

app.UseAuthorization();
app.MapControllers();
app.Run();
