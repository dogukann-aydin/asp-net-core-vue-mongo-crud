using ASP.NETCoreWebAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// MongoDB'yi DI konteynerine ekle
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

// CORS yapýlandýrmasý
builder.Services.AddCors();

builder.Services.AddControllers();

// Swagger/OpenAPI için gerekli servisler
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI'yi etkinleþtir
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS kullan
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:8080") // Vue.js'in çalýþtýðý adres
          .AllowAnyHeader()
          .AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
