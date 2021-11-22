using ClothesApplicationMicroservice.Web.Application.Commands;
using ClothesApplicationMicroservice.Web.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Application.DataServiceClients
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly string host;
        private readonly string port;
        private readonly IGeneralServiceClient serviceClient;
        public UserServiceClient(IGeneralServiceClient serviceClient)
        {
            this.host = Environment.GetEnvironmentVariable("CLOTHES_DATA_API");
            this.port = Environment.GetEnvironmentVariable("CLOTHES_DATA_API_PORT");
            this.serviceClient = serviceClient;
        }
        public async Task<TokenDto> Login(UserDataDto passes)
        {
            string uri = $"http://{host}:{port}/login";
            TokenDto result = await serviceClient.PostData<TokenDto>(uri, passes);
            return result;
        }

        public async Task<TokenDto> Register(AddUserCommand command)
        {
            string uri = $"http://{host}:{port}/register";
            TokenDto result = await serviceClient.PostData<TokenDto>(uri, command);
            return result;
        }
    }
}
