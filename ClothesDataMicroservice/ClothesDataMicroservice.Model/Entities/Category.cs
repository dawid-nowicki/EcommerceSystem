using System.Collections.Generic;

namespace ClothesDataMicroservice.Model.Entities
{
    public class Category : Entity
    {
        public CategoryEnum Value { get; private set; }
        public IEnumerable<Cloth> Clothes { get; private set; }
        public Category(long id, CategoryEnum value) : base(id)
        {
            this.Value = value;
        }
    }
}
