using ClothesApplicationMicroservice.Web.Application.DataServiceClients;
using ClothesApplicationMicroservice.Web.Application.Dtos;
using ClothesApplicationMicroservice.Web.Application.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ApplicationController : ControllerBase
    {
        private readonly IClothesServiceClient clothesServiceClient;
        private readonly CitiesDataReader citiesDataReader;
        public ApplicationController(IClothesServiceClient clothesServiceClient, CitiesDataReader citiesDataReader)
        {
            this.citiesDataReader = citiesDataReader;
            this.clothesServiceClient = clothesServiceClient;
        }
        [HttpGet("get-categories")]
        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            return await clothesServiceClient.GetCategories();
        }
        [HttpGet("get-all-clothes")]
        public async Task<IEnumerable<ClothDto>> GetAllClothes([FromHeader] string authorization)
        {
            IEnumerable<ClothDto> clothes = await clothesServiceClient.GetAllClothes();

            if (authorization != null)
            {
                IEnumerable<ClothDto> userClothes = await clothesServiceClient.GetCurrentUserClothes(authorization);
                PossessionChecker.Check(clothes, userClothes);
            }
            return clothes;
        }
        [HttpGet("get-clothes-by-category")]
        public async Task<IEnumerable<ClothDto>> GetClothesByCategory([FromHeader] string authorization, [FromQuery] string categoryId)
        {
            IEnumerable<ClothDto> clothes = await clothesServiceClient.GetClothesByCategory(categoryId);

            if (authorization != null)
            {
                IEnumerable<ClothDto> userClothes = await clothesServiceClient.GetCurrentUserClothes(authorization);
                PossessionChecker.Check(clothes, userClothes);
            }
            return clothes;
        }
        [HttpGet("get-my-clothes")]
        public async Task<IActionResult> GetUserClothes([FromHeader] string authorization)
        {
            if (!string.IsNullOrEmpty(authorization))
            {
                var result = await clothesServiceClient.GetCurrentUserClothes(authorization);
                return Ok(result);
            }

            return Unauthorized();
        }

        [HttpGet("buy")]
        public async Task<IActionResult> BuyCloth([FromHeader] string authorization, [FromQuery] long clothId)
        {
            if (!string.IsNullOrEmpty(authorization))
            {
                var result = await clothesServiceClient.BuyClothes(clothId, authorization);
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpGet("get-outfits-by-localization")]
        public async Task<IEnumerable<IEnumerable<ClothDto>>> GetOutfitsByLocalization([FromQuery] string localization, [FromHeader] string authorization)
        {
            if (string.IsNullOrEmpty(localization))
            {
                return null;
            }
            IEnumerable<IEnumerable<ClothDto>> clothes = await clothesServiceClient.GetOutfitByLocalization(localization);
            if (!string.IsNullOrEmpty(authorization))
            {
                IEnumerable<ClothDto> userClothes = await clothesServiceClient.GetCurrentUserClothes(authorization);
                foreach (var cloths in clothes)
                {
                    PossessionChecker.Check(cloths, userClothes);
                }
            }          
            return clothes;
        }
        [HttpGet("get-cities")]
        public IEnumerable<CityDto> GetCitiesStartsWithWord([FromQuery] string word)
        {
            return citiesDataReader.GetCitiesStartWithWord(word);
        }
    }
}
