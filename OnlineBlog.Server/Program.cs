using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineBlog.Server.Data;
using OnlineBlog.Server.Helpers;
using OnlineBlog.Server.Services;
using OnlineBlog.Server.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<UsersService>();
builder.Services.AddTransient<NewsService>();
builder.Services.AddTransient<LikesService>();
builder.Services.AddTransient<SubsService>();
builder.Services.AddTransient<IdentityService>();
builder.Services.AddTransient<NoSQLDataService>();
builder.Services.AddTransient<Mapping>();

// Добавление jwt токена
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });

// Подключение к БД SQL
string connection = builder.Configuration.GetConnectionString("mssqllocaldb");
builder.Services.AddDbContext<AppDataContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        var basePath = AppContext.BaseDirectory;
        // Формируем путь к XML-документации
        var xmlPath = Path.Combine(basePath, "OnlineBlog.xml");
        // Включаем комментарии XML-документации для контроллеров
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        // Определяем конфигурацию Swagger-документации с названием и описанием версии
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Онлайн-блог",
            Description = "ASP.NET Core Web API для приложения онлайн-блога (ASP.NET + React)"
        });
    });

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
