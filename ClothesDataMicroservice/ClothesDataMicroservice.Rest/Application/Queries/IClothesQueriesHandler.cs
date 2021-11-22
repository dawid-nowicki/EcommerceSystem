using ClothesDataMicroservice.Rest.Application.Dtos;
using System.Collections.Generic;

namespace ClothesDataMicroservice.Rest.Application.Queries
{
    public interface IClothesQueriesHandler
    {
        public IEnumerable<ClothDto> GetAll();
        public IEnumerable<ClothWithCategoryDto> GetByWeatherAndTemperature(double temperature, bool isSnowy, bool isRainy);
        public IEnumerable<ClothDto> GetByCategory(long category);
        public IEnumerable<ClothWithCategoryDto> GetByCurrentUser(string email);
        public IEnumerable<CategoryDto> GetCategories();
        public bool AddClothToUser(long clothId, string userEmail);

    }
}
