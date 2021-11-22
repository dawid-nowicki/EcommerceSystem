using ClothesApplicationMicroservice.Web.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Application.DataServiceClients
{
    public interface IClothesServiceClient
    {
        public Task<IEnumerable<ClothDto>> GetAllClothes();
        public Task<IEnumerable<CategoryDto>> GetCategories();
        public Task<IEnumerable<ClothDto>> GetClothesByCategory(string categoryId);
        public Task<IEnumerable<ClothDto>> GetCurrentUserClothes(string token);
        public Task<IEnumerable<IEnumerable<ClothDto>>> GetOutfitByLocalization(string localization);
        public Task<bool> BuyClothes(long clothId, string token);
    }
}
