using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RP_Ovning15.Data.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LmsApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LmsApiContext") ?? throw new InvalidOperationException("Connection string 'LmsApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers( opt => opt.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

var app = builder.Build();

app.SeedDataAsync().GetAwaiter().GetResult();

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
