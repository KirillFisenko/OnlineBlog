using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineBlog.Server.Data;
using OnlineBlog.Server.Models;
using OnlineBlog.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<UsersService>();

// ƒобавление jwt токена
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироватьс€ издатель при валидации токена
            ValidateIssuer = true,
            // строка, представл€юща€ издател€
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироватьс€ потребитель токена
            ValidateAudience = true,
            // установка потребител€ токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироватьс€ врем€ существовани€
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидаци€ ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });

// ѕодключение к Ѕƒ
string connection = builder.Configuration.GetConnectionString("mssqllocaldb");
builder.Services.AddDbContext<AppDataContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
