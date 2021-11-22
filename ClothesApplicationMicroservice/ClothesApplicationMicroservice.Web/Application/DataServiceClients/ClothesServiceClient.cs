using ClothesApplicationMicroservice.Web.Application.Dtos;
using ClothesApplicationMicroservice.Web.Application.Utilities;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Application.DataServiceClients
{
    public class ClothesServiceClient : IClothesServiceClient
    {
        private readonly string host;
        private readonly string port;
        private readonly IGeneralServiceClient serviceClient;
        private readonly IWeatherServiceClient weatherServiceClient;
        public ClothesServiceClient(IWeatherServiceClient weatherServiceClient, IGeneralServiceClient serviceClient)
        {
            this.host = Environment.GetEnvironmentVariable("CLOTHES_DATA_API");
            this.port = Environment.GetEnvironmentVariable("CLOTHES_DATA_API_PORT");
            this.weatherServiceClient = weatherServiceClient;
            this.serviceClient = serviceClient;
        }
        public async Task<IEnumerable<ClothDto>> GetAllClothes()
        {
            var uri = $"http://{host}:{port}/get-all-clothes";
            IEnumerable<ClothDto> result = await serviceClient.GetData<IEnumerable<ClothDto>>(uri);
            return result;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var uri = $"http://{host}:{port}/get-categories";
            IEnumerable<CategoryDto> result = await serviceClient.GetData<IEnumerable<CategoryDto>>(uri);
            return result;
        }

        public async Task<IEnumerable<ClothDto>> GetClothesByCategory(string categoryId)
        {
            var uri = QueryHelpers.AddQueryString($"http://{host}:{port}/get-clothes-by-category",
                new Dictionary<string, string> { { "category", categoryId } });

            IEnumerable<ClothDto> result = await serviceClient.GetData<IEnumerable<ClothDto>>(uri);
            return result;
        }

        private async Task<IEnumerable<ClothDto>> getClothesByLocalWeather(WeatherTemperatureDto weather)
        {
            string uri = $"http://{host}:{port}/get-clothes-by-weather";
            IEnumerable<ClothDto> result = await serviceClient.PostData<IEnumerable<ClothDto>>(uri, weather);
            return result;
        }


        public async Task<IEnumerable<IEnumerable<ClothDto>>> GetOutfitByLocalization(string localization)
        {
            WeatherTemperatureDto weather = await weatherServiceClient.GetWeatherByLocalization(localization);
            if(weather == null)
            {
                return null;
            }

            IEnumerable<IEnumerable<Category>> outfitsTemplates = OutfitFactory.Build(weather);
            IEnumerable<ClothDto> suitabeClothes = await getClothesByLocalWeather(weather);

            IEnumerable<IEnumerable<ClothDto>> results = MakeOutfits(suitabeClothes, outfitsTemplates);
            return results;
        }

        public async Task<IEnumerable<ClothDto>> GetCurrentUserClothes(string token)
        {
            var uri = $"http://{host}:{port}/get-current-user-clothes";
            IEnumerable<ClothDto> result = await serviceClient.GetDataWithAuthorization<IEnumerable<ClothDto>>(uri, token);
            return result;
        }

        public async Task<bool> BuyClothes(long clothId, string token)
        {
            var uri = $"http://{host}:{port}/buy?clothId={clothId}";

            bool result = await serviceClient.GetDataWithAuthorization<bool>(uri, token);
            return result;
        }

        private IEnumerable<IEnumerable<ClothDto>> MakeOutfits(IEnumerable<ClothDto> suitabeClothes, IEnumerable<IEnumerable<Category>> outfitsTemplates)
        {
            List<List<ClothDto>> results = new List<List<ClothDto>>();
            foreach (IEnumerable<Category> template in outfitsTemplates)
            {
                List<ClothDto> outfit = new List<ClothDto>();
                foreach (Category category in template)
                {
                    foreach (ClothDto cloth in suitabeClothes)
                    {
                        if (category.ToString().Equals(cloth.Category))
                        {
                            outfit.Add(cloth);
                            break;
                        }
                    }
                }
                if (template.Count() == outfit.Count())
                {
                    results.Add(outfit);
                }
            }
            return results;
        }
    }
}
