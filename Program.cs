using MONGO_DB_API.Data;
using MONGO_DB_API.Repositories.Implementations;
using MONGO_DB_API.Repositories.Interfaces;
using MONGO_DB_API.Services.Implementations;
using MONGO_DB_API.Services.Interfaces;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Mappings.MappingProfiles;
using MONGO_DB_API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<MongoDBContext>();
builder.Services.AddTransient<IEntityRepository<Employee>, EntityRepository<Employee>>();
builder.Services.AddTransient<IEntityRepository<Department>, EntityRepository<Department>>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();

// Register AutoMapper with your profiles
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddLogging(builder =>
{
    builder.AddProvider(new DatabaseLoggerProvider(
        (category, level) => level >= LogLevel.Information, // Example filter configuration
        builder.Services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>()
    ));
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
