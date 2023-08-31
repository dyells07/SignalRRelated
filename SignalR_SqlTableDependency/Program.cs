using Microsoft.EntityFrameworkCore;
using SignalR_SqlTableDependency.BL;
using SignalR_SqlTableDependency.Hubs;
using SignalR_SqlTableDependency.MiddlewareExtensions;
using SignalR_SqlTableDependency.Models;
using SignalR_SqlTableDependency.Repositories;
using SignalR_SqlTableDependency.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();




//DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SignalRWithEFContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Singleton
);

// DI
builder.Services.AddSingleton<UserRepo>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();
builder.Services.AddSingleton<SubscribeSaleTableDependency>();
builder.Services.AddSingleton<SubscribeCustomerTableDependency>();
//builder.Services.AddSingleton<AdminHub>();
builder.Services.AddSingleton<AdminJobs>();



// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

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

app.UseSession();
app.MapHub<DashboardHub>("/dashboardHub");
//app.MapHub<DashboardHub>("/adminHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

/*
 * we must call SubscribeTableDependency() here
 * we create one middleware and call SubscribeTableDependency() method in the middleware
 */
app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeProductTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeSaleTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeCustomerTableDependency>(connectionString);

app.Run();