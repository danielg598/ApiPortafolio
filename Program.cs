using ApiPortafolio.Data;
using ApiPortafolio.Services.DependencyInyection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAplicationServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200", //localhost angular
            "https://cute-muffin-32ef60.netlify.app/", //produccion netlify
            "http://localhost:5173") // localhost react
                  .AllowAnyMethod()  // Permitir GET, POST, PUT, DELETE, etc.
                  .AllowAnyHeader()  // Permitir cualquier encabezado
                  .AllowCredentials(); // Permitir cookies/autenticación 
    });
});
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer("name=dataBaseConection"));

var app = builder.Build();

app.UseCors("AllowAngularClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
