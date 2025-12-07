using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddUserSecrets<Program>(optional: true) // Local
    .AddEnvironmentVariables(); // Docker

// Obter connection string pela ordem de prioridade:
// 1. Variáveis de ambiente (Docker)
// 2. User Secrets (local)
// 3. appsettings (fallback)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine("Connection string carregada:");
Console.WriteLine(connectionString);

// Registrar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<MovieCatalog.Middlewares.ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
