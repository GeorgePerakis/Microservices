using Common.MongoBD;
using Common.Settings;
using MicroServices.Service.Entities;

var builder = WebApplication.CreateBuilder(args);

// Bind ServiceSettings from appsettings.json or other sources
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddMongo().AddMongoRepository<Item>("Items");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();


app.MapGet("/", () => "Test Endpoint")
   .WithName("Test")
   .WithOpenApi();

app.Run();
