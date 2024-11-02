namespace Provider
{
    public static class Extensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Add services to the container
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Register application services
            services.AddSingleton<IStudentRepository, StudentRepository>();
        }

        public static void Configure(this WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            //app.UseEndpoints(e => e.MapControllers());
        }
    }
}
