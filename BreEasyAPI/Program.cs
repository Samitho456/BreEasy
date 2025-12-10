using Microsoft.EntityFrameworkCore;
using BreEasy;
using BreEasy.EFDbContext;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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


bool useLocal = false;

if (useLocal == false)
{
    builder.Services.AddDbContext<WindowDbContext>(options =>
    {
        var conn = builder.Configuration.GetConnectionString("BreEasyDB");
        //var conn = Environment.GetEnvironmentVariable("DATABASE_URL");
        options.UseMySql(conn, ServerVersion.AutoDetect(conn));
        // Replace (8, 0, 21) with your actual MySQL version (e.g., 5.7 or 8.0)
        //var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
        //options.UseMySql(conn, serverVersion);
    });
} else
{
    // add local functionality here
}

// Register the WindowsDbRepo for dependency injection
builder.Services.AddScoped<WindowsDbRepo, WindowsDbRepo>();
builder.Services.AddScoped<LocationDbRepo, LocationDbRepo>();




var app = builder.Build();

app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
