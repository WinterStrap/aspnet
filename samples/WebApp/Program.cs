using System.Reflection;
using WinterStrap.AspNet.Samples.Feature.Cities;
using WinterStrap.AspNet.Samples.WebApp;

//var builder = WebApplication.CreateBuilder(args);
var builder= WinterStrapApplicationBuilder.CreateBuilder(args);

var citiesAssembly = Assembly.GetAssembly(typeof(CitiesController)) ?? throw new Exception();

builder.Services.AddControllers()
    .AddApplicationPart(citiesAssembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddDependencies(builder.Configuration);

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
