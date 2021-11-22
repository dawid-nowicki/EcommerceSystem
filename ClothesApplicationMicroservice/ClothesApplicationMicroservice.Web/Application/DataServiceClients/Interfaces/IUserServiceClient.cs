using ClothesApplicationMicroservice.Web.Application.Commands;
using ClothesApplicationMicroservice.Web.Application.Dtos;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Application.DataServiceClients
{
    public interface IUserServiceClient
    {
        public Task<TokenDto> Login(UserDataDto passes);
        public Task<TokenDto> Register(AddUserCommand command);

    }
}
