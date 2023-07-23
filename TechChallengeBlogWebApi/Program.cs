using App.Blog.Application.Services;
using App.Blog.Infra.Context;
using App.Blog.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NoticiaDBContext>(o => o.UseSqlServer(configuration.GetConnectionString("AzureBdContext")), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

builder.Services.AddScoped(typeof(NoticiaDBContext));
builder.Services.AddScoped(typeof(NoticiaRepository));
builder.Services.AddScoped(typeof(NoticiaService));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
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
});

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();