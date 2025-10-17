using Fanex.BetList.Core;
using Fanex.BetList.Core.RemotingRef;
using Fanex.RemotingRef;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Web API support (replaces WebApiConfig)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

RefDataTypeContainer
            .ConnectTo(new Uri(configuration["RemotingRefURL"]))
            .FromApplication(Assembly.GetExecutingAssembly().FullName);

builder.Services.AddScoped<Fanex.BetList.Example.MVC.Services.DataService>()
    .AddSingleton<IRemotingRefService, RemotingRefService>();

BetListManager.RegisterRemotingRef(new CacheRef());
BetListManager.Verify();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseSession();

app.UseAuthorization();

// Map MVC routes (replaces RouteConfig)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BetList}/{action=All}/{id?}");

// Map Web API routes (replaces WebApiConfig)
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
