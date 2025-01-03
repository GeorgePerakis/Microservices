using Common.MongoBD;
using Services.Inventory.Clients;
using Services.Inventory.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongo().AddMongoRepository<InventoryItem>("inventoryitems");
builder.Services.AddControllers();
builder.Services.AddHttpClient<CatalogClient>(client => {
    client.BaseAddress = new Uri("http://localhost:5231");
});


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

