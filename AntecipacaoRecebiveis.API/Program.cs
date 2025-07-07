using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Domain.Interfaces.Data;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
using AntecipacaoRecebiveis.Domain.Interfaces.Services;
using AntecipacaoRecebiveis.Infrastructure;
using AntecipacaoRecebiveis.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<INotaFiscalService, NotaFiscalService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Convert the enum into string
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
