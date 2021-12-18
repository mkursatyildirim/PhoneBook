using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Entities.Context;
using PhoneBook.API.Middlewares;
using PhoneBook.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PhoneBookContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("ConString")));
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IContactInformationService, ContactInformationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
