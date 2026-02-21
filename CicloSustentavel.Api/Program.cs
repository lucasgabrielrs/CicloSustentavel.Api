using CicloSustentavel.Application.Services;
using CicloSustentavel.Domain.Repositories;
using CicloSustentavel.Infrastructure.Data;
using CicloSustentavel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=/app/data/CicloSustentavel.db";

if (string.IsNullOrWhiteSpace(connectionString))
{
    if (builder.Environment.IsDevelopment())
    {
        connectionString = "Data Source=../CicloSustentavel.Infrastructure/CicloSustentavel.db";
    }
    else
    {
        connectionString = "Data Source=/app/data/CicloSustentavel.db";
    }
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    DbInitializer.Seed(context);
}

app.Run();