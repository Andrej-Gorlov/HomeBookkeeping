using HomeBookkeeping.Web;
using HomeBookkeeping.Web.Services.Implementations.HomeBookkeepingService;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<IÑreditÑardService, ÑreditÑardService>();
builder.Services.AddHttpClient<IUserService, UserService>();
builder.Services.AddHttpClient<ITransactionService, TransactionService>();
builder.Services.AddHttpClient<IReportServise, ReportServise>();

StaticDitels.HomeBookkeepingApiBase = builder.Configuration["ServiseUrl:HomeBookkeepingAPI"];

builder.Services.AddScoped<IÑreditÑardService, ÑreditÑardService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IReportServise, ReportServise>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
