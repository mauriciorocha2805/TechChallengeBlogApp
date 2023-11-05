

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TechChallengeBlogWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}



//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using TechChallengeBlogWebApp.Services;
//using TechChallengeBlogWebApp.Util;

//#nullable disable

////WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
//{
//    EnvironmentName = Environments.Development
//});


//ConfigurationManager configuration = builder.Configuration;

//// Add services to the container.
//builder.Services.AddRazorPages();

//builder.Services.AddHttpClient<BlogService>(client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
//});

//builder.Services.Configure<ApiBlogConfig>(configuration.GetSection("ApiBlogConfig"));

//WebApplication app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

////app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();
