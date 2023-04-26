using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Square;
using Square.Apis;

namespace YourNamespace
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Square API client
            services.AddSingleton<ISquareClient>(client =>
            {
                return new SquareClient.Builder()
                    .Environment(Square.Environment.Sandbox)
                    .AccessToken(Configuration["Square:AccessToken"])
                    .Build();
            });

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors();
        }
    }
}
