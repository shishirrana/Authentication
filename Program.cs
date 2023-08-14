using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using reviseAuthentication.Controllers;
using reviseAuthentication.Controllers.API;
using reviseAuthentication.Data;
using reviseAuthentication.Filter;
using reviseAuthentication.Settings;
using System.Configuration;
using reviseAuthentication.Controllers.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("reviseAuthentication") ?? throw new InvalidOperationException("Connection string 'comportfoliowebsiteContext' not found.")));

//Adding Singleton

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

//Authentication Roles and Policy using Cookie

builder.Services.AddAuthentication("Cookies")
    .AddCookie(config =>
    {
        config.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        config.LoginPath = "/Home/Login"; //path to redirect user login path
        config.AccessDeniedPath = "/Home/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});


//Customize AuthorizationAttribute

builder.Services.AddMvc((options) =>
{
    options.Filters.Add(typeof(MyCustomAuthorizeAttribute));
    options.Filters.Add(typeof(MyActionFilter));
    options.Filters.Add(typeof(MyExceptionFilter));

});



//Email
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//Mapping for MinimalAPI
app.MapGet("api/myapi/getData", () =>
{
    return new
    {
        name = "Shishir",
        address = "Kathmandu"
    };
});

//UserMinimalAPI
app.MapEndpoints();

//Routing for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Routing for API

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Home}/{action=Index}/{id?}");

app.MapStudentModelEndpoints();

app.Run();
