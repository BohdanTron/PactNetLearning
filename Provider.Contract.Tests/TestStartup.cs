using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Contract.Tests.Middlewares;
using Provider.Controllers;

namespace Provider.Contract.Tests;

public class TestStartup
{
    public TestStartup(IConfiguration configuration)
    {
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IStudentRepository, StudentRepository>();
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            })
            .PartManager.ApplicationParts.Add(new AssemblyPart(typeof(StudentsController).Assembly));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ProviderStateMiddleware>();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}