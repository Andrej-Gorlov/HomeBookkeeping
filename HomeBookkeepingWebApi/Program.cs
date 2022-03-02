using AutoMapper;
using HomeBookkeepingWebApi;
using HomeBookkeepingWebApi.DAL;
using HomeBookkeepingWebApi.DAL.Interfaces;
using HomeBookkeepingWebApi.DAL.Repository;
using HomeBookkeepingWebApi.Service.Implementations;
using HomeBookkeepingWebApi.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
// Add services to the container.

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();// create object mapping
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//��������� AutoMapper


builder.Services.AddScoped<I�redit�ardRepository, �redit�ardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<I�redit�ardService, �redit�ardService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IReportService, ReportService>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HomeBookkeepingWebApi",
        Description = "������ Web API (�������� �����������).",
        TermsOfService = new Uri("https://yandex.ru"),
        Contact = new OpenApiContact
        {
            Name = "������ ������",
            Email = "avgorlov899@gmail.com",
            Url = new Uri("https://github.com/Andrej-Gorlov?tab=repositories")
        },
        License = new OpenApiLicense
        {
            Name = "��������...",
            Url = new Uri("https://yandex.ru")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});



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
