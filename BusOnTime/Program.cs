using BusOnTime.Application.Interfaces;
using BusOnTime.Application.Services;
using BusOnTime.Data.Interfaces.Interface;
using BusOnTime.Data.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repository 

builder.Services.AddScoped<IEquipmentR, EquipmentR>();
builder.Services.AddScoped<IEquipmentModelR, EquipmentModelR>();
builder.Services.AddScoped<IEquipmentPositionHistoryR, EquipmentPositionHistoryR>();
builder.Services.AddScoped<IEquipmentStateHistoryR, EquipmentStateHistoryR>();
builder.Services.AddScoped<IEquipmentStateR, EquipmentStateR>();
builder.Services.AddScoped<IEquipmentModelStateHourlyEarningsR, EquipmentModelStateHourlyEarningsR>();
//////////////////////

//Services

builder.Services.AddScoped<IEquipmentS, EquipmentS>();
builder.Services.AddScoped<IEquipmentModelS, EquipmentModelS>();
builder.Services.AddScoped<IEquipmentPositionHistoryS, EquipmentPositionHistoryS>();
builder.Services.AddScoped<IEquipmentStateHistoryS, EquipmentStateHistoryS>();
builder.Services.AddScoped<IEquipmentStateS, EquipmentStateS>();
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
