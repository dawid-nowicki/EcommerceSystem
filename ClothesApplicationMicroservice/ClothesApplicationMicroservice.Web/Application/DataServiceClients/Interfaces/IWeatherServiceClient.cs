using ClothesApplicationMicroservice.Web.Application.Dtos;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Application.DataServiceClients
{
    public interface IWeatherServiceClient
    {
        Task<WeatherTemperatureDto> GetWeatherByLocalization(string localization);
    }
}
