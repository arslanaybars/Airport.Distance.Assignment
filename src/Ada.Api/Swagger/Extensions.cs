using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ada.Api.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(configuration["Swagger:Version"], new OpenApiInfo { Title = configuration["Swagger:Title"], Version = configuration["Swagger:Version"] });
            });
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{configuration["Swagger:Version"]}/swagger.json", configuration["Swagger:Title"]);
                c.RoutePrefix = string.Empty;
            });

            return builder;
        }
    }
}
