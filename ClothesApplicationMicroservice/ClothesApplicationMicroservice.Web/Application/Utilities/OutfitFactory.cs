using ClothesApplicationMicroservice.Web.Application.Dtos;
using System.Collections.Generic;

namespace ClothesApplicationMicroservice.Web.Application.Utilities
{
    public class OutfitFactory
    {
        public static IEnumerable<IEnumerable<Category>> Build(WeatherTemperatureDto weather)
        {
            List<Category> firstOutfitTemplate = new List<Category>();
            List<Category> secoundOutfitTemplate = new List<Category>();

            if (weather.Temperature < 0)
            {
                firstOutfitTemplate.Add(Category.JACKETS);
                firstOutfitTemplate.Add(Category.SWEATSHIRTS);
                firstOutfitTemplate.Add(Category.SHIRTS);
                firstOutfitTemplate.Add(Category.TROUSERS);
                firstOutfitTemplate.Add(Category.SHOES);

                return new List<IEnumerable<Category>>() { firstOutfitTemplate };
            }
            else if(weather.Temperature < 8)
            {
                firstOutfitTemplate.Add(Category.JACKETS);
                secoundOutfitTemplate.Add(Category.JACKETS);
                firstOutfitTemplate.Add(Category.SWEATSHIRTS);
                  firstOutfitTemplate.Add(Category.SHIRTS);
                firstOutfitTemplate.Add(Category.TROUSERS);

                secoundOutfitTemplate.Add(Category.DRESSES);
            }
            else if (weather.Temperature < 15)
            {
                if (weather.IsRainy)
                {
                    firstOutfitTemplate.Add(Category.JACKETS);
                    secoundOutfitTemplate.Add(Category.JACKETS);
                }
                firstOutfitTemplate.Add(Category.SWEATSHIRTS);
                firstOutfitTemplate.Add(Category.SHIRTS);
                firstOutfitTemplate.Add(Category.TROUSERS);

                secoundOutfitTemplate.Add(Category.DRESSES);
            }
            else { 
                secoundOutfitTemplate.Add(Category.DRESSES);
                firstOutfitTemplate.Add(Category.SWEATSHIRTS);
                firstOutfitTemplate.Add(Category.SHIRTS);
                firstOutfitTemplate.Add(Category.TROUSERS);

            }
            firstOutfitTemplate.Add(Category.SHOES);
            secoundOutfitTemplate.Add(Category.SHOES);

            return new List<IEnumerable<Category>>() { firstOutfitTemplate, secoundOutfitTemplate };
        }
    }
}
