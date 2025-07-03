using AntecipacaoRecebiveis.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
using AntecipacaoRecebiveis.Infrastructure.Repositories;
using AntecipacaoRecebiveis.Domain.Interfaces.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("DefaultConnection").Value));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Convert the enum into string
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
