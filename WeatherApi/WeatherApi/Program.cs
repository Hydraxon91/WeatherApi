using WeatherApi.Context;
using WeatherApi.Model;
using WeatherApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options => 
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IWeatherDataProvider, OpenWeatherMapApi>();
builder.Services.AddSingleton<IJsonProcessor, JsonProcessor>();

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

void InitializeDb()
{
    using var db = new WeatherApiContext();
    InitializeCities();
    PrintCities();

    void InitializeCities()
    {
        db.Add(new City { Name = "London", Latitude = 51.509865, Longitude = -0.118092 });
        db.Add(new City { Name = "Budapest", Latitude = 47.497913, Longitude = 19.040236 });
        db.Add(new City { Name = "Paris", Latitude = 48.864716, Longitude = 2.349014 });
        db.SaveChanges();
    }

    void PrintCities()
    {
        foreach (var city in db.Cities)
        {
            Console.WriteLine($"{city.Id}, {city.Name}, {city.Latitude}, {city.Longitude}");
        }
    }
}

InitializeDb();


app.Run();