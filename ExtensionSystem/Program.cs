using AutoMapper;
using Core.CacheServices;
using Data.ConsumerExpenseRepositories;
using Data.ConsumerRepositories;
using Data.ExpenseRepositories;
using Domain.EFCoreDbContext;
using ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.CacheServices.ConsumerCacheServices;
using Services.CacheServices.ConsumerExpenceCacheServices;
using Services.CacheServices.ExpencesCacheServices;
using Services.Services.ConsumerExpenseServices;
using Services.Services.ConsumerSerivces;
using Services.Services.ExpenseServices;
using Services.Services.Mappings;
using Services.Services.TokenServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // JWT ile kimlik doðrulama yapýlandýrmasý
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token in the field below",
        Name = "Authorization"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = "https://localhost",
        ValidAudience = "https://localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DemoProjectSecretKey")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddMemoryCache();
builder.Services.AddDbContext<ExpenseSystemDbContext>(options =>
    options.UseSqlServer("server =.\\SQLEXPRESS; database = ExpenseTracking; integrated security = true; TrustServerCertificate = True"));

builder.Services.AddTransient<IconsumerExpenceService, ConsumerExpenseService>();
builder.Services.AddTransient<IExpenseService, ExpenseService>();
builder.Services.AddTransient<IConsumerService, ConsumerService>();
builder.Services.AddTransient<ITokenService, TokenService>();


builder.Services.AddTransient<IConsumerExpenseRepository, ConsumerExpenseRepository>();
builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IConsumerRepository, ConsumerRepository>();


builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddTransient<IExpenceCacheService, ExpenceCacheService>();
builder.Services.AddTransient<IConsumerCacheServicecs, ConsumerCacheService>();
builder.Services.AddTransient<IConsumerExpenceCacheService, ConsumerExpenceCacheService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var configuration = new MapperConfiguration(opt =>
{
    opt.AddProfile(new ConsumerExpencesProfile());
    opt.AddProfile(new ConsumerProfile());
    opt.AddProfile(new ExpenceProfile());
});

var mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

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

