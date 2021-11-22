using System.Collections.Generic;

namespace ClothesDataMicroservice.Model.Entities
{
    public class Cloth : Entity
    {
        public string Name { get; private set; }
        public string ImagePath { get; private set; }
        public double Price { get; private set; }
        public Category Category { get; private set; }
        public WeatherConditions WeatherConditions { get; private set; }
        public List<User> Owners { get; private set; }
        public Cloth(long id) : base(id)
        {
            
        }

        public Cloth(long id, string name, string imagePath, double price, Category category, WeatherConditions weatherConditions):base(id)
        {
            this.Name = name;
            this.ImagePath = imagePath;
            this.Price = price;
            this.Category = category;
            this.WeatherConditions = weatherConditions;
        }
    }
}
