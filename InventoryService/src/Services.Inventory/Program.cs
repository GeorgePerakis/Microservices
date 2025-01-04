using Common.MongoBD;
using Polly;
using Polly.Timeout;
using Services.Inventory.Clients;
using Services.Inventory.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongo().AddMongoRepository<InventoryItem>("inventoryitems");
builder.Services.AddControllers();

//Added Randomness so that if there are a lot of inventory instances they will not be retrying on the same exact time. 
//This can help not overwhelm the Server.
Random jitterer = new Random();

builder.Services.AddHttpClient<CatalogClient>(client => {
    client.BaseAddress = new Uri("http://localhost:5231");
})
.AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
    5,
    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0,1000))
))
.AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().CircuitBreakerAsync(
    3,
    TimeSpan.FromSeconds(15)
))
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();


app.Run();

