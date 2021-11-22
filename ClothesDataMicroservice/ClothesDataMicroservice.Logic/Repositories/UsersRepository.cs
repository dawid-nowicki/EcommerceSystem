using ClothesDataMicroservice.Model.DataBase;
using ClothesDataMicroservice.Model.Entities;
using ClothesDataMicroservice.Model.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClothesDataMicroservice.Logic.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public bool AddClothToUserClothes(string userEmail, long clothId)
        {
            using (var context = new DatabaseContext())
            {
                User user = context.Users.Where(x => x.Email.Equals(userEmail)).Include(x => x.Clothes).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                Cloth cloth = context.Clothes.Where(x => x.Id.Equals(clothId)).FirstOrDefault();
                if (cloth == null)
                {
                    return false;
                }
                user.Clothes.Add(cloth);
                context.SaveChanges();
            }
            return true;
        }

        public void AddUser(User user)
        {
            using (var context = new DatabaseContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var context = new DatabaseContext())
            {
                return context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            }
        }
    }
}
