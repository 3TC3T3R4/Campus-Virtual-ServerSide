using AutoMapper.Data;
using CampusVirtual.API.AutoMapper;
using CampusVirtual.Infrastructure.SQLAdapter;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.Infrastructure.SQLAdapter.Repositories;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using CampusVirtual.UseCases.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(ConfigurationProfile));

builder.Services.AddScoped<IDeliveryUseCase, DeliveryUseCase>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IContentUseCase, ContentUseCase>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();

builder.Services.AddTransient<IDbConnectionBuilder>(e =>
{
    return new DbConnectionBuilder(builder.Configuration.GetConnectionString("urlConnectionSQL"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
