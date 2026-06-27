using HolidayAssessment.Clients;
using HolidayAssessment.Data;
using HolidayAssessment.Repositories;
using HolidayAssessment.Services;
using HolidayAssessment.Validators;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<HolidayDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<INagerApiClient, NagerApiClient>(client =>
{
    client.BaseAddress = new Uri("https://date.nager.at/api/v3/");
});

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICountryCacheService, CountryCacheService>();
builder.Services.AddScoped<IHolidayRepository, HolidayRepository>();
builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<CountrySeeder>();
builder.Services.AddScoped<CountryValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CountrySeeder>();
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
