using ClothesDataMicroservice.Rest.Application.Dtos;
using ClothesDataMicroservice.Rest.Application.Queries;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ClothesDataMicroservice.Rest.Controllers
{
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IClothesQueriesHandler clothesQueriesHandler;
        public DataController(IClothesQueriesHandler clothesQueriesHandler)
        {
            this.clothesQueriesHandler = clothesQueriesHandler;
        }

        [HttpGet("get-all-clothes")]
        public IEnumerable<ClothDto> GetAllClothes()
        {
            return clothesQueriesHandler.GetAll();
        }

        [HttpGet("get-categories")]
        public IEnumerable<CategoryDto> GetCategories()
        {
            return clothesQueriesHandler.GetCategories();
        }

        [HttpGet("get-clothes-by-category")]
        public IEnumerable<ClothDto> GetClothesByCategory([FromQuery] long category)
        {
            return clothesQueriesHandler.GetByCategory(category);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("get-current-user-clothes")]
        public IEnumerable<ClothWithCategoryDto> GetCurrentUserClothes()
        {
            var username = User.Claims.SingleOrDefault();
            return clothesQueriesHandler.GetByCurrentUser(username.Value);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("buy")]
        public bool BuyCloth([FromQuery] long clothId)
        {
            var username = User.Claims.SingleOrDefault();
            return clothesQueriesHandler.AddClothToUser(clothId, username.Value);
        }

        [HttpPost("get-clothes-by-weather")]
        public IEnumerable<ClothWithCategoryDto> GetClothesByWeatherAndTemperature([FromBody] WeatherTemperatureDto weatherTemperature)
        {
            return clothesQueriesHandler.GetByWeatherAndTemperature(weatherTemperature.Temperature, weatherTemperature.IsSnowy, weatherTemperature.IsRainy);
        }
    }
}
