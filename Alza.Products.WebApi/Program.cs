using Alza.Products.Application.Interfaces;
using Alza.Products.Application.Services;
using Alza.Products.Infrastructure.Configuration;
using Alza.Products.Infrastructure.Context;
using Alza.Products.Infrastructure.Repositories;
using Alza.Products.WebApi.Filters;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Reflection;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(new GlobalExceptionFilter());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Alza Products API",
                Version = "v1"
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "Alza Products API",
                Version = "v2"
            });

            // for reading endpoint comments
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        builder.Services.AddDbContext<ProductDbContext>(options => options
            .UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "Alza Products API v1");
                options.SwaggerEndpoint("v2/swagger.json", "Alza Products API v2");
            });

            var dbInitOptions = app.Configuration
                .GetSection("Database")
                .Get<DatabaseInitializationOptions>();

            await using var scope = app.Services.CreateAsyncScope();
            await using (var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>())
            {
                if (dbInitOptions?.EnsureDeleted == true)
                    await dbContext.Database.EnsureDeletedAsync();

                if (dbInitOptions?.EnsureCreated == true)
                {
                    await dbContext.Database.EnsureCreatedAsync();

                    if (dbInitOptions?.SeedData == true)
                        await ProductDbSeeder.SeedAsync(dbContext);
                }
            }

            app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}