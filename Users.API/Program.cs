using AutoMapper.Data;
using Users.API.AutoMapper;
using Users.Infrastructure;
using Users.Infrastructure.MongoAdapter.Interfaces;
using Users.Infrastructure.MongoAdapter.Repositories;
using Users.UseCases.Gateway;
using Users.UseCases.Gateway.Repositories;
using Users.UseCases.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(ConfigurationProfile));


builder.Services.AddSingleton<IContext>(provider => new Context(builder.Configuration.GetConnectionString("urlConnectionMongo"), "CampusVirtual"));

builder.Services.AddScoped<IUserUseCase, UserUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


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
