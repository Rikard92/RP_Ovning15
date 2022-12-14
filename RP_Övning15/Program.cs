using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RP_Ovning15.Core.Repositories;
using RP_Ovning15.Data.Data;
using RP_Ovning15.Data.Repositories;

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
builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddAutoMapper(typeof(LmsMappings));

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
