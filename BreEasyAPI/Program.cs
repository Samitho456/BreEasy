using Microsoft.EntityFrameworkCore;
using BreEasy;
using BreEasy.EFDbContext;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<WindowDbContext>(options =>
{
    //  var conn = builder.Configuration.GetConnectionString("DefaultConnection
    var conn = Environment.GetEnvironmentVariable("DATABASE_URL");
    options.UseMySql(conn, ServerVersion.AutoDetect(conn));
});

// Register the WindowsDbRepo for dependency injection
builder.Services.AddScoped<WindowsDbRepo, WindowsDbRepo>();
builder.Services.AddScoped<LocationDbRepo, LocationDbRepo>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
