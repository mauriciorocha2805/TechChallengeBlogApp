using App.Application.Interfaces;
using App.Application.Services;
using App.Blog.Application.Interfaces;
using App.Blog.Application.Services;
using App.Blog.Infra.Context;
using App.Blog.Infra.Interfaces;
using App.Blog.Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TechChallengeBlogWebApi.Factory;
using TechChallengeBlogWebApi.Interfaces;
using TechChallengeBlogWebApi.Services;
using TechChallengeBlogWebApi.Util;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<NoticiaDBContext>(o => o.UseSqlServer(configuration.GetConnectionString("AzureBdContext")), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

builder.Services.AddScoped(typeof(NoticiaDBContext));

builder.Services.AddScoped<INoticiaRepository, NoticiaRepository>();
builder.Services.AddScoped<ISistemaRepository, SistemaRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<INoticiaService, NoticiaService>();
builder.Services.AddScoped<ISistemaService, SistemaService>();

builder.Services.AddSingleton<IRsaSecurityKeyFactory, RsaSecurityKeyFactory>();
builder.Services.AddScoped(provider =>
{
    var factory = provider.GetRequiredService<IRsaSecurityKeyFactory>();
    return factory.Create();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
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

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.ConfigureRsaSecurityKeyFactory(configuration);
    });

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();