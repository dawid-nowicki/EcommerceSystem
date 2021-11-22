using ClothesDataMicroservice.Model.Entities;
using ClothesDataMicroservice.Rest.Application.Dtos;

namespace ClothesDataMicroservice.Rest.Application.Mapper
{
    public static class Mapper
    {
        public static ClothDto Map(this Cloth cloth)
        {
            if (cloth != null)
                return new ClothDto()
                {
                    Id = cloth.Id.ToString(),
                    Name = cloth.Name,
                    ImagePath = cloth.ImagePath,
                    Price = cloth.Price.ToString()
                };
            return null;
        }

        public static ClothWithCategoryDto MapWithCategory(this Cloth cloth)
        {
            if (cloth != null)
                return new ClothWithCategoryDto()
                {
                    Id = cloth.Id.ToString(),
                    Name = cloth.Name,
                    ImagePath = cloth.ImagePath,
                    Price = cloth.Price.ToString(),
                    Category = cloth.Category.Value.ToString()
                };
            return null;
        }
     
        public static CategoryDto Map(this Category category)
        {
            if (category != null)
                return new CategoryDto
                {
                    Id = category.Id.ToString(),
                    CategoryValue = category.Value.ToString()
                };
            return null;
        }
    }
}
