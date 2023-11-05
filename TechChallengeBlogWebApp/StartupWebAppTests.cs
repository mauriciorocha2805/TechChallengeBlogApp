
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TechChallengeBlogWebApp.Services;
using TechChallengeBlogWebApp.Util;

namespace TechChallengeBlogWebApp
{
    public class StartupWebAppTests
    {
        public IConfiguration Configuration { get; }

        public StartupWebAppTests(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddHttpClient<BlogService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"] ?? string.Empty);
            });

            services.Configure<ApiBlogConfig>(Configuration.GetSection("ApiBlogConfig"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            //if (!env.IsDevelopment())
            //{
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
