using ClothesApplicationMicroservice.Web.Application.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ClothesApplicationMicroservice.Web.Application.Utilities
{
    public class CitiesDataReader
    {
        private readonly string citiesFilePath = "Application/Statics/cities.json";

        public IEnumerable<CityDto> GetCitiesStartWithWord(string word)
        {
            List<CityDto> cities =  DeserializeFile();
            return GetFiltredCities(cities, word);
        }
        private List<CityDto> DeserializeFile()
        {
            var json = File.ReadAllText(citiesFilePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            return  JsonSerializer.Deserialize<List<CityDto>>(json, options);
        }

        private IEnumerable<CityDto> GetFiltredCities(List<CityDto> cities, string filter)
        {
            List<CityDto> result = new List<CityDto>();
            foreach (CityDto city in cities)
            {
                if (result.Count == 15)
                {
                    return result;
                }
                else if (city.City.ToLower().StartsWith(filter.ToLower()))
                {
                    result.Add(city);
                }
            }
            return result;
        }
    }
}
