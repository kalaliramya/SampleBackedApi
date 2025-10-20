using Microsoft.EntityFrameworkCore;
using Samplebacked_api.Controllers;
using Samplebacked_api.EFCore;
using Samplebacked_api.Model.GdriveService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<GoogleDriveHelper>();

builder.Services.AddDbContext<patientDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("EF_Postgres_DB")));
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
