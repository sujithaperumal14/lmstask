//Library Management System
//Created at:10/01/2023


using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Data.Repositories;
using Serilog;

namespace LibraryManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
var builder = WebApplication.CreateBuilder(args);
//builder.Host.ConfigureLogging(logging=>{
  //  logging.ClearProviders();
    //logging.AddConsole();
//});
// builder.Services.AddLogging(loggingBuilder =>
// {
//     loggingBuilder.ClearProviders();
//     loggingBuilder.AddFile("logs/LibLogger.txt");
// });
var configuration=builder.Configuration;
string? connection=builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<BookDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<UserDetailDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<OrderDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<BorrowBookDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<IssueBookDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
builder.Services.AddDbContext<BorrowDbContext>(options=>{
    options.UseSqlServer(connection).EnableSensitiveDataLogging();
});
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var apiEndpoint = configuration["ApiEndpoint"];
            builder.Services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri(apiEndpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("LibraryManagementSystem/json"));
            });

var _logger=new LoggerConfiguration()
.WriteTo.File("C:\\Users\\Dell\\application\\LibraryManagementSystem\\Logs\\LibLogger.log",rollingInterval:RollingInterval.Day)
.CreateLogger();

builder.Logging.AddSerilog(_logger);
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                    options.LoginPath = new PathString("/Account/Login");
                });


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

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
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}