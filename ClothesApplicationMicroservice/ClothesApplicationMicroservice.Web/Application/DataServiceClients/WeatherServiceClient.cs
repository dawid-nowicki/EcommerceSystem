using ClothesApplicationMicroservice.Web.Application.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Application.DataServiceClients
{
    public class WeatherServiceClient : IWeatherServiceClient
    {
        private readonly string secretKey;
        private readonly IGeneralServiceClient serviceClient;

        public WeatherServiceClient(IHttpClientFactory clientFactory, IGeneralServiceClient serviceClient)
        {
            this.serviceClient = serviceClient;
            this.secretKey = Environment.GetEnvironmentVariable("SECRET_WEATHER_API");
        }
        public async Task<WeatherTemperatureDto> GetWeatherByLocalization(string localization)
        {
            var uri = $"https://api.openweathermap.org/data/2.5/weather?q={localization}&appid={secretKey}&units=metric";
            var response = await serviceClient.GetHttpResponseMessage(uri);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var streamReader = new StreamReader(responseStream);
            return convertToWeatherTemperatureDto(streamReader.ReadToEnd());
        }

        private WeatherTemperatureDto convertToWeatherTemperatureDto(string json)
        {
            WeatherTemperatureDto result = new WeatherTemperatureDto();
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            var jObject = JObject.Load(reader);

            var validation = jObject.GetValue("message");

            if(validation != null && validation.Value<string>().Equals("city not found"))
            {
                return null;
            }

            JObject temperature = (JObject)jObject.GetValue("main");

            result.Temperature = temperature.GetValue("feels_like").Value<double>();
            result.IsSnowy = jObject.GetValue("snow") != null;
            result.IsRainy = jObject.GetValue("rain") != null;

            return result;
        }
    }
}
