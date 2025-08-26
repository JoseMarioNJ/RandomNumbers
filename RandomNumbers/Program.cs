using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore; // 👈 importar Scalar

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // 👈 importante para Scalar

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Habilitar Scalar UI
app.MapOpenApi();
app.MapScalarApiReference(); // 👈 UI de Scalar en /scalar

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();


