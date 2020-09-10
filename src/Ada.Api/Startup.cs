using Ada.Api.ExceptionHandler;
using Ada.Api.Filters;
using Ada.Api.HttpClient;
using Ada.Api.Models;
using Ada.Api.Services;
using Ada.Api.Swagger;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ada.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(typeof(ModelStateApiFilterAttribute)))
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<AirportCodes>();
                });
            services.AddIataGeoClient(Configuration);
            services.AddScoped<IAirportDistanceService, AirportDistanceService>();
            services.AddSwagger(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandlingMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
