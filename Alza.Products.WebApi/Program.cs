using Alza.Products.Infrastructure.Context;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "Alza Products API v1");
                options.SwaggerEndpoint("v2/swagger.json", "Alza Products API v2");
            });

            // create DB and seed data
            await using (var scope = app.Services.CreateAsyncScope())
            await using (var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>())
            {
                // uncomment to delete DB
                //await dbContext.Database.EnsureDeletedAsync();

                await dbContext.Database.EnsureCreatedAsync();
                await ProductDbSeeder.SeedAsync(dbContext);
            }
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}