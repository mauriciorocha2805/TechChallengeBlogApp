using App.Application.Interfaces;
using App.Application.Services;
using App.Blog.Application.Interfaces;
using App.Blog.Application.Services;
using App.Blog.Infra.Context;
using App.Blog.Infra.Interfaces;
using App.Blog.Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using TechChallengeBlogWebApi.Factory;
using TechChallengeBlogWebApi.Interfaces;
using TechChallengeBlogWebApi.Services;
using TechChallengeBlogWebApi.Util;

namespace TechChallengeBlogWebApi
{
    public class StartupApiTests
    {
        public IConfiguration Configuration { get; }

        public StartupApiTests(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<NoticiaDBContext>(o => o.UseSqlServer(Configuration.GetConnectionString("AzureBdContext")), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            services.AddScoped(typeof(NoticiaDBContext));

            services.AddScoped<INoticiaRepository, NoticiaRepository>();
            services.AddScoped<ISistemaRepository, SistemaRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INoticiaService, NoticiaService>();
            services.AddScoped<ISistemaService, SistemaService>();

            services.AddSingleton<IRsaSecurityKeyFactory, RsaSecurityKeyFactory>();
            services.AddScoped(provider =>
            {
                var factory = provider.GetRequiredService<IRsaSecurityKeyFactory>();
                return factory.Create();
            });

            services.AddScoped<JwtSecurityExtensionEvents>();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blog Notícias API",
                    Description = "An ASP.NET Core Web API for managing Notícias items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Grupo MMMC",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, Array.Empty<string>()
                    }
                });
            });

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.ConfigureRsaSecurityKeyFactory(Configuration);
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
