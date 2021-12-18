using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.API.Constants;
using Report.API.Entities.Context;
using Report.API.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ReportContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("ConString")));
builder.Services.Configure<ReportSettings>(builder.Configuration.GetSection("Options"));
builder.Services.AddHttpClient();

var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetRequiredService<ReportContext>().Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRabbitMq();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
