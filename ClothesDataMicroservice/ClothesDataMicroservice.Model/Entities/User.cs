using System.Collections.Generic;

namespace ClothesDataMicroservice.Model.Entities
{
    public class User : Entity
    {
        public string Email { get; private set; }
        public string Hash { get; private set; }
        public List<Cloth> Clothes { get;  set; }

        public User(long id, string email, string hash) : base(id)
        {
            this.Email = email;
            this.Hash = hash;
        }
        public User(string email, string hash)
        {
            this.Email = email;
            this.Hash = hash;
        }
    }
}
