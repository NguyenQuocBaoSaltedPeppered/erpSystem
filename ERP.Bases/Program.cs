using ERP.Bases.Models;
using ERP.Databases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<DataContext>
    (
        option => option.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
    );
builder.Services.AddScoped<IUserModel, UserModel>();
builder.Services.AddScoped<IBranchModel, BranchModel>();
builder.Services.AddScoped<IDepartmentModel, DepartmentModel>();
builder.Services.AddScoped<IPositionModel, PositionModel>();
builder.Services.AddScoped<ILogModel, LogModel>();

var app = builder.Build();
// DataSeeder later

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

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Namespace
// {
//     public class Program
//     {
//         /// <summary>
//         /// Tên của microservice hiện tại
//         /// </summary>
//         public static readonly string? ServiceName = typeof(Program).Namespace;
//         public static void Main(string[] args)
//         {

//         }
//         // public static IHostBuilder CreateHostBuilder(string[] args)
//         // {
//         //     Host.CreateDefaultBuilder(args)
//         //         .UseServiceProviderFactory(new AutofacServiceProviderFactory())
//         // }
//     }
// }