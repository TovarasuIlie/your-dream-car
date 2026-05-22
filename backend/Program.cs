using backend.DatabaseContext;
using backend.Repositories;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LocalConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<QuizService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "sub",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var result = System.Text.Json.JsonSerializer.Serialize(new
                {
                    message = "Nu esti autorizat sa accesezi acesta resursa. Te rugam autentifica-te!"
                });

                return context.Response.WriteAsync(result);
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var result = System.Text.Json.JsonSerializer.Serialize(new
                {
                    message = "Nu ai permisiunile necesare pentru a accesa acesta resursa!"
                });

                return context.Response.WriteAsync(result);
            }
        };
    });

builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["ClientUrl"]);
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        var databaseCreator = context.Database.GetService<IRelationalDatabaseCreator>();

        if (!databaseCreator.Exists())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "A apărut o eroare fatală la verificarea/crearea bazei de date.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
