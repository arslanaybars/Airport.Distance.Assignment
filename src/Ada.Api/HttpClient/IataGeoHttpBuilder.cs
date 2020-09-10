using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ada.Api.HttpClient
{
    public static class IataGeoHttpBuilder
    {
        public static IServiceCollection AddIataGeoClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<IataGeoOptions>(configuration.GetSection("IataGeo"));
            services.AddHttpClient<IIataGeoClient, IataGeoClient>();
            return services;
        }
    }
}
