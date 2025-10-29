using WeatherApi.Repositories;
using WeatherApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000", "http://localhost:5002")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddHttpClient<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

app.MapControllers();

app.UseCors("AllowReactApp");

app.Run();
