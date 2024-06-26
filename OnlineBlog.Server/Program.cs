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

// ���������� jwt ������
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,
            // ������, �������������� ��������
            ValidIssuer = AuthOptions.ISSUER,
            // ����� �� �������������� ����������� ������
            ValidateAudience = true,
            // ��������� ����������� ������
            ValidAudience = AuthOptions.AUDIENCE,
            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
        };
    });

// ����������� � �� SQL
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
        // ��������� ���� � XML-������������
        var xmlPath = Path.Combine(basePath, "OnlineBlog.xml");
        // �������� ����������� XML-������������ ��� ������������
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        // ���������� ������������ Swagger-������������ � ��������� � ��������� ������
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "������-����",
            Description = "ASP.NET Core Web API ��� ���������� ������-����� (ASP.NET + React)"
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
