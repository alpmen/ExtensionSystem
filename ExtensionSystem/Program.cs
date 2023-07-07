using AutoMapper;
using Data.ConsumerExpenseRepositories;
using Data.ConsumerRepositories;
using Data.ExpenseRepositories;
using Domain.EFCoreDbContext;
using ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Services.ConsumerExpenseServices;
using Services.Services.ConsumerSerivces;
using Services.Services.ExpenseServices;
using Services.Services.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ExpenseSystemDbContext>(options =>
    options.UseSqlServer("server =.\\SQLEXPRESS; database = ExpenseTracking; integrated security = true; TrustServerCertificate = True"));

builder.Services.AddTransient<IconsumerExpenceService, ConsumerExpenseService>();
builder.Services.AddTransient<IExpenseService, ExpenseService>();
builder.Services.AddTransient<IConsumerService, ConsumerService>();


builder.Services.AddTransient<IConsumerExpenseRepository, ConsumerExpenseRepository>();
builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IConsumerRepository, ConsumerRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
