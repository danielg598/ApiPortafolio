using ApiPortafolio.Data;
using ApiPortafolio.Entities;
using ApiPortafolio.Services.DependencyInyection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de SMTP personalizada
builder.Services.Configure<SmtpSettings>(options =>
{
    var configuration = builder.Configuration;
    var env = builder.Environment;

    options.Host = configuration.GetValue<string>("Smtp:Host") ?? Environment.GetEnvironmentVariable("SMTP_HOST");
    options.Port = configuration.GetValue<int?>("Smtp:Port") ?? int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
    options.User = configuration.GetValue<string>("Smtp:User") ?? Environment.GetEnvironmentVariable("SMTP_USER");
    options.Pass = configuration.GetValue<string>("Smtp:Pass") ?? Environment.GetEnvironmentVariable("SMTP_PASS");
});

// Configurar cadena de conexión
string connectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("dataBaseConection");
}
else
{
    connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    }));

builder.Services.AddAplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200", // localhost Angular
            "http://localhost:5173", // localhost React
            "https://cute-muffin-32ef60.netlify.app" // producción Netlify
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activar CORS
app.UseCors("AllowAngularClient");

// Swagger en dev y prod
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
