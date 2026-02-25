using CicloSustentavel.Application.Services;
using CicloSustentavel.Domain.Repositories;
using CicloSustentavel.Domain.Security.Cryptography;
using CicloSustentavel.Domain.Security.Tokens;
using CicloSustentavel.Infrastructure.Data;
using CicloSustentavel.Infrastructure.Repositories;
using CicloSustentavel.Infrastructure.Security.Cryptography;
using CicloSustentavel.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("WebAppPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? (builder.Environment.IsDevelopment()
        ? "Data Source=../CicloSustentavel.Infrastructure/CicloSustentavel.db"
        : "Data Source=/app/data/CicloSustentavel.db");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IPasswordEncrypt, EncryptBcrypt>();

// Configuração JWT
var jwtSettings = builder.Configuration.GetSection("Settings:Jwt");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey não configurada");
var expiresTimeMinutes = uint.Parse(jwtSettings["ExpiresTimeMinutes"] ?? "60");

builder.Services.AddScoped<IAccesTokenGenerator>(provider => 
    new JwtTokenGenerator(expiresTimeMinutes, secretKey));

var app = builder.Build();

app.UseCors("WebAppPolicy");

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