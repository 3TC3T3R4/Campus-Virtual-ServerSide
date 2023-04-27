using AutoMapper.Data;
using CampusVirtual.API.AutoMapper;
using CampusVirtual.API.Middlewares;
using CampusVirtual.Infrastructure.SQLAdapter;
using CampusVirtual.Infrastructure.SQLAdapter.Gateway;
using CampusVirtual.Infrastructure.SQLAdapter.Repositories;
using CampusVirtual.UseCases.Gateway;
using CampusVirtual.UseCases.Gateway.Repositories;
using CampusVirtual.UseCases.UseCases;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.WithOrigins("http://localhost:4200")
							.SetIsOriginAllowedToAllowWildcardSubdomains()
							.AllowAnyHeader()
							.AllowAnyMethod();
					  });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(ConfigurationProfile));

builder.Services.AddScoped<ICourseUseCase, CourseUseCase>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IDeliveryUseCase, DeliveryUseCase>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IContentUseCase, ContentUseCase>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<IRegistrationUseCases, RegistrationUseCases>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<ILearningPathUseCase, LearningPathUseCase>();
builder.Services.AddScoped<ILearningPathRepository, LearningPathRepository>();


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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();

app.Run();
