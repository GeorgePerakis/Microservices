using MicroServices.Service.Repositories;
using MicroServices.Service.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Bind ServiceSettings from appsettings.json or other sources
builder.Services.Configure<ServiceSettings>(
    builder.Configuration.GetSection("ServiceSettings"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(ServiceProvider => {
    var serviceSettings = ServiceProvider.GetRequiredService<IConfiguration>()
    .GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
    
    var mongoDbSettings = ServiceProvider.GetRequiredService<IConfiguration>()
    .GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    
    var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
    
    return mongoClient.GetDatabase(serviceSettings.ServiceName);
});
builder.Services.AddSingleton<IItemsRepository,ItemsRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// Register MongoDB serializers
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

app.MapGet("/", () => "Test Endpoint")
   .WithName("Test")
   .WithOpenApi();

app.Run();
