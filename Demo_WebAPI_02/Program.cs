using Demo_WebAPI_02.Repositories;
using Microsoft.Data.SqlClient;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

const string CONNECTION_STRING = "Data Source=ICT-204-00;Initial Catalog=astro_db;Integrated Security=True;Trust Server Certificate=True";

builder.Services.AddCors(options =>
{
    // Ajout une régle -> Tout autorisé (dev uniquement)
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});
// Add services to the container (C'est magic -> On vera ca en dev :p)
builder.Services.AddTransient<PlanetRepository>();
builder.Services.AddTransient<SolarSystemRepository>();
builder.Services.AddTransient<StarRepository>();
builder.Services.AddTransient<DbConnection>(proviver =>
{
    DbConnection connection = new SqlConnection(CONNECTION_STRING);
    connection.Open();
    return connection;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
