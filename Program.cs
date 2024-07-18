//using Governement_Public_Health_Care;
//using Governement_Public_Health_Care.DB_Context;
//using Governement_Public_Health_Care.Models;
//using Microsoft.EntityFrameworkCore;
//using System;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //auto mapping is mapping up things and the inner part is scanning and confirming it that this current domain is being mapped


//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

////Add DB context
//builder.Services.AddDbContext<HealthCareContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

////builder.Services.AddScoped<IGenericInterface<DiseaseRegistry, int>, GenericRepository<DiseaseRegistry, int>>();


//void ConfigureServices(IServiceCollection services)
//{
//    // Other service configurations...

//    // Register your interface and its implementation for DI
//    services.AddScoped<IGenericInterface<DiseaseRegistry,int>, GenericRepository<DiseaseRegistry, int>>();
//}

//var app = builder.Build();



//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



using Governement_Public_Health_Care;
using Governement_Public_Health_Care.DB_Context;
using Governement_Public_Health_Care.Models;
using Governement_Public_Health_Care.NewFolder;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using Governement_Public_Health_Care.LogErrors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<IGenericInterface<DiseaseRegistry, int>, GenericRepository<DiseaseRegistry, int>>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB context
builder.Services.AddDbContext<HealthCareContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Errors Service
builder.Services.AddScoped<ErrorsFile>();

// Shared Data Service for signaling
builder.Services.AddSingleton<SharedDataService>();
builder.Services.AddHostedService<ScopedBasedBackGroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
