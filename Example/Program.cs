using Example.Business;
using Example.Data;
using Example.Data.Services;
using Example.DTO.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string? connectionStrings = builder.Configuration.GetConnectionString("ExampleDb");
builder.Services.AddDbContext<DbContext,ExampleMSContext> (options=> options.UseSqlServer (connectionStrings));


builder.Services.AddEntities();
builder.Services.AddDataService();
builder.Services.AddBusinessService();
builder.Services.AddMapperServices();



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
