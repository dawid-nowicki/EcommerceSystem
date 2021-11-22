using ClothesDataMicroservice.Model.Entities;
using System.Collections.Generic;

namespace ClothesDataMicroservice.Model.IRepositories
{
    public interface IClothesRepository
    {
        public IEnumerable<Cloth> GetAllClothes();

        public IEnumerable<Cloth> GetClothesByWeatherAndTemperature(double temperature, bool isSnowy, bool isRainy);
        public IEnumerable<Category> GetCategories();
        public Category GetCategoryById(long id);
        public IEnumerable<Cloth> GetUserClothesByEmail(string email);
    }
}
