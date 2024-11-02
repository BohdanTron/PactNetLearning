using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Provider.Controllers;

namespace Provider
{
    public static class Extensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(typeof(StudentsController).Assembly));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IStudentRepository, StudentRepository>();

            return builder;
        }

        public static IApplicationBuilder Configure(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
