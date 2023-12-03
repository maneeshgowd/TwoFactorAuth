using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TwoFactorAuth.Controllers;
using TwoFactorAuth.DBContext;
using TwoFactorAuth.Interfaces;
using TwoFactorAuth.SecretsModel;
using TwoFactorAuth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSecret:Key").Value!)),
            ValidateAudience = false,
            ValidateIssuer = true,
        };
    });

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// configuring Options configuration
builder.Services.Configure<SmtpSecrets>(SmtpSecrets.Bravo, builder.Configuration.GetSection("SmtpSecrets:Bravo"));
builder.Services.Configure<SmtpSecrets>(SmtpSecrets.Mailtrap, builder.Configuration.GetSection("SmtpSecrets:Mailtrap"));
builder.Services.Configure<JwtSecret>(builder.Configuration.GetSection(nameof(JwtSecret)));
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

AuthController.Map(app);

app.Run();
