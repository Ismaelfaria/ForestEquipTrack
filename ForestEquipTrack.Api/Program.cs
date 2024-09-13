using ForestEquipTrack.Application.Interfaces;
using ForestEquipTrack.Application.Mapping.DTOs.InputModel;
using ForestEquipTrack.Application.Mapping.Profiles;
using ForestEquipTrack.Application.Services;
using ForestEquipTrack.Application.Validators;
using ForestEquipTrack.Infrastructure.DataContext;
using ForestEquipTrack.Infrastructure.Interfaces.Interface;
using ForestEquipTrack.Infrastructure.Repositories.Concrete;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString = builder.Configuration.GetConnectionString("BusOnTimeConnection");

builder.Services.AddDbContext<Context>(o => o.UseSqlServer(ConnectionString));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions
        (
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
        );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ProfileMapping));

//FluentValidation

builder.Services.AddTransient<IValidator<EquipmentIM>, EquipmentInputValidator>();
builder.Services.AddTransient<IValidator<EquipmentModelIM>, EquipmentModelInputValidator>();
builder.Services.AddTransient<IValidator<EquipmentModelStateHourlyEarningsIM>, EquipmentModelStateHourlyEarningsInputValidator>();
builder.Services.AddTransient<IValidator<EquipmentPositionHistoryIM>, EquipmentPositionHistoryInputValidator>();
builder.Services.AddTransient<IValidator<EquipmentStateHistoryIM>, EquipmentStateHistoryInputValidator>();

///////////////////////

//Repository 

builder.Services.AddScoped<IEquipmentR, EquipmentR>();
builder.Services.AddScoped<IEquipmentModelR, EquipmentModelR>();
builder.Services.AddScoped<IEquipmentPositionHistoryR, EquipmentPositionHistoryR>();
builder.Services.AddScoped<IEquipmentStateHistoryR, EquipmentStateHistoryR>();
builder.Services.AddScoped<IEquipmentModelStateHourlyEarningsR, EquipmentModelStateHourlyEarningsR>();
//////////////////////

//Services

builder.Services.AddScoped<IEquipmentS, EquipmentS>();
builder.Services.AddScoped<IEquipmentModelS, EquipmentModelS>();
builder.Services.AddScoped<IEquipmentPositionHistoryS, EquipmentPositionHistoryS>();
builder.Services.AddScoped<IEquipmentStateHistoryS, EquipmentStateHistoryS>();
builder.Services.AddScoped<IEquipmentModelStateHourlyEarningS, EquipmentModelStateHourlyEarningS>();

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
