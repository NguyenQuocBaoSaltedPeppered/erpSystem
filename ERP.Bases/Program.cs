using ERP.Bases.Models;
using ERP.Databases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "ERP System",
        builder =>
        {
            builder.WithOrigins("http://localhost:9001")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyMethod();
        });
    });
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
builder.Services.AddScoped<IAuthModel, AuthModel>();

var app = builder.Build();
// DataSeeder later

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>{
    options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
