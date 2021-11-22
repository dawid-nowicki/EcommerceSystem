using ClothesDataMicroservice.Model.Entities;
using ClothesDataMicroservice.Model.IRepositories;
using ClothesDataMicroservice.Rest.Application.Dtos;
using ClothesDataMicroservice.Rest.Application.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace ClothesDataMicroservice.Rest.Application.Queries
{
    public class ClothesQueriesHandler : IClothesQueriesHandler
    {
        private readonly IClothesRepository clothesRepository;
        private readonly IUsersRepository usersRepository;
        public ClothesQueriesHandler(IClothesRepository clothesRepository, IUsersRepository usersRepository)
        {
            this.clothesRepository = clothesRepository;
            this.usersRepository = usersRepository;
        }

        public IEnumerable<ClothDto> GetAll()
        {
            var tmp = clothesRepository.GetAllClothes();
            return tmp.Select(c => c.Map());
        }

        public IEnumerable<ClothWithCategoryDto> GetByWeatherAndTemperature(double temperature, bool isSnowy, bool isRainy)
        {
            return clothesRepository.GetClothesByWeatherAndTemperature(temperature, isSnowy, isRainy)?.Select(c => c.MapWithCategory());
        }

        public IEnumerable<ClothDto> GetByCategory(long categoryNum)
        {
            return clothesRepository.GetCategoryById(categoryNum)?.Clothes.Select(c => c.Map());
        }
    
        public IEnumerable<ClothWithCategoryDto> GetByCurrentUser(string email)
        {
            return clothesRepository.GetUserClothesByEmail(email)?.Select(c => c.MapWithCategory());
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            return clothesRepository.GetCategories()?.Select(c => c.Map());
        }

        public bool AddClothToUser(long clothId, string userEmail)
        {
            return usersRepository.AddClothToUserClothes(userEmail, clothId);
        }
    }
}
