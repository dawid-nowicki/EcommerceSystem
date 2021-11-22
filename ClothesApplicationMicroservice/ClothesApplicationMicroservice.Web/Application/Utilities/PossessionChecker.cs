using ClothesApplicationMicroservice.Web.Application.Dtos;
using System.Collections.Generic;

namespace ClothesApplicationMicroservice.Web.Application.Utilities
{
    public class PossessionChecker
    {
        public static IEnumerable<ClothDto> Check(IEnumerable<ClothDto> allClothes, IEnumerable<ClothDto> userClothes)
        {
            foreach(ClothDto cloth in allClothes)
            {
                foreach(ClothDto userCloth in userClothes)
                {
                    if (cloth.Id.Equals(userCloth.Id))
                    {
                        cloth.isUserOwn = true;
                    }
                }
            }
            return allClothes;
        }
    }
}
