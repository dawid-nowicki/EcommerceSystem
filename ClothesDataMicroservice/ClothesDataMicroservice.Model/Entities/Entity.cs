using System.ComponentModel.DataAnnotations;

namespace ClothesDataMicroservice.Model.Entities
{
    public abstract class Entity
    {
        [Key]
        public long Id { get; protected set; }

        public Entity(long id)
        {
            this.Id = id;
        }
        public Entity()
        {

        }
    }
}
