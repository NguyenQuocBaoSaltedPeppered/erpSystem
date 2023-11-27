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
builder.Services.AddScoped<IUserModel, UserModel>(provider =>
{
    var context = provider.GetRequiredService<DataContext>();
    var logger = provider.GetRequiredService<ILogger<UserModel>>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = builder.Configuration.GetConnectionString("Default");

    return new UserModel(context, logger, connectionString);
});
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
