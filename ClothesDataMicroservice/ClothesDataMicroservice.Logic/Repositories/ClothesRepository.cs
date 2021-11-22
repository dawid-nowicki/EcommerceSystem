using ClothesDataMicroservice.Model.DataBase;
using ClothesDataMicroservice.Model.Entities;
using ClothesDataMicroservice.Model.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ClothesDataMicroservice.Logic.Repositories
{
    public class ClothesRepository : IClothesRepository
    {
        public IEnumerable<Cloth> GetAllClothes()
        {
            using (var context = new DatabaseContext())
            {
                return context.Clothes.AsNoTracking().ToList();
            }
        }

        public IEnumerable<Cloth> GetClothesByWeatherAndTemperature(double temperature, bool isSnowy, bool isRainy)
        {
            using (var context = new DatabaseContext())
            {
                return context.Clothes.Include(cl => cl.WeatherConditions).Include(cl => cl.Category).AsNoTracking().ToList()
                    .Where(c => c.WeatherConditions.IsTemperatureBetweenMinMax(temperature) &&
                    c.WeatherConditions.IsSuitableOnThisWeather(isSnowy, isRainy));
            }
        }

        public IEnumerable<Category> GetCategories()
        {

            using (var context = new DatabaseContext())
            {
                return context.Categories.AsNoTracking().ToList();
            }
        }

        public Category GetCategoryById(long id)
        {
            using (var context = new DatabaseContext())
            {
                return context.Categories.Where(x => x.Id.Equals(id))
                                         .Include(x => x.Clothes)
                                         .AsNoTracking()
                                         .FirstOrDefault();
            }
        }

        public IEnumerable<Cloth> GetUserClothesByEmail(string email)
        {
            using (var context = new DatabaseContext())
            {
                var user = context.Users.Where(x => x.Email.Equals(email))
                                           .AsNoTracking()
                                           .Include(c => c.Clothes)
                                           .ThenInclude(x => x.Category)
                                           .FirstOrDefault();
                return user.Clothes;
            }
        }
    }
}
