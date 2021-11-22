using ClothesApplicationMicroservice.Web.Application.DataServiceClients;
using ClothesApplicationMicroservice.Web.Application.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace ClothesApplicationMicroservice
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IGeneralServiceClient, GeneralServiceClient>();
            services.AddTransient<IUserServiceClient, UserServiceClient > ();
            services.AddTransient<IClothesServiceClient, ClothesServiceClient>();
            services.AddTransient<IWeatherServiceClient, WeatherServiceClient>();
            services.AddTransient<CitiesDataReader>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                .AllowAnyHeader().AllowAnyMethod());

            });
            services.AddHttpClient();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("AllowOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
