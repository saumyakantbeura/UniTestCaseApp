using MediatR;
using Microsoft.EntityFrameworkCore;

using UniTestCaseApp.Data;

//using CQRSMediatr.Services;
using UniTestCaseApp.Services.Employee.Domain;
using UniTestCaseApp.Services.Employee.Repository.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// Add services to the container.



builder.Services.AddControllers();
//builder.Services.AddMediatR(Configuration =>
//{
//    Configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
//}

//);


// Register DbContext (AppDbContext) with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"))
);

// Register the repository (EmployeeRepository)
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Register MediatR and scan for handlers in the same assembly as Program.cs
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
