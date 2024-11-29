using MongoDB.Driver;
using Sub.Data.Models;
using Sub.Data;
using Sub.Repository.Repositories;
using Sub.Repository;
using Sub.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.AppDomain.CurrentDomain.BaseDirectory}{System.IO.Path.DirectorySeparatorChar}{builder.Environment.ApplicationName}.xml";
    options.IncludeXmlComments(xmlFile);
});

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDb");
    var client = new MongoClient(connectionString);
    return new MongoDbContext(client, "Sub");
});

builder.Services.AddScoped<IRepository<Tarefa>, TarefaRepository>();
builder.Services.AddScoped<TarefaService>();

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
