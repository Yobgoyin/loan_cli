
using applicant_api.Context;
using applicant_api.Model.Logic;
using applicant_api.Model.Logic.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IApplicantService, ApplicantService>();
builder.Services.AddTransient<IApplicantRepository, ApplicantRepository>();

var connectionString = builder.Configuration.GetConnectionString("DockerDB");
builder.Services.AddDbContext<ApplicationQuoteContext>(options => {
    options.UseSqlServer(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:8081",
                                                  "http://localhost:5173")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//db ef migration in docker
DBHandler.InitMigration(app);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(MyAllowSpecificOrigins);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
