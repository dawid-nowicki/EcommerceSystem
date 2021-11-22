using ClothesDataMicroservice.Model.Entities;

namespace ClothesDataMicroservice.Model.IRepositories
{
    public interface IUsersRepository
    {
        public void AddUser(User user);
        public User GetUserByEmail(string email);
        public bool AddClothToUserClothes(string userEmail, long clothId);
    }
}
