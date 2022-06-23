using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Data;
using MyWebApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<MyDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));

//var secretKey = builder.Configuration["AppSettings:SecretKey"];
builder.Services.Configure<AppSettings>
    (builder.Configuration.GetSection("AppSettings"));

ConfigurationManager configuration = builder.Configuration;
var k = configuration["AppSettings:SecretKey"];

var secretKeyByte = Encoding.UTF8.GetBytes(k);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //tu cap token
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //Ky vao token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
                        ClockSkew = TimeSpan.Zero
                    };
                });


builder.Services.AddScoped<ILoaiRepository, LoaiRepository>();
//tao lop tinh de test
//builder.Services.AddScoped<ILoaiRepository, LoaiRepositoryInMemory>();
builder.Services.AddScoped<IHangHoaRepository, HangHoaRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
IConfiguration conFiguration = app.Configuration;


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
