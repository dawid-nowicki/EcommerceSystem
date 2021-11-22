using ClothesApplicationMicroservice.Web.Application.Commands;
using ClothesApplicationMicroservice.Web.Application.DataServiceClients;
using ClothesApplicationMicroservice.Web.Application.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserServiceClient userServiceClient;

        public AuthenticationController(IUserServiceClient commandHandler)
        {
            this.userServiceClient = commandHandler;
        }
        [HttpPost("register")]
        public async Task<TokenDto> Register([FromBody] AddUserCommand command)
        {
            return  await userServiceClient.Register(command);
        }
        [HttpPost("login")]
        public async Task<TokenDto> Login([FromBody] UserDataDto userData)
        {
            return await userServiceClient.Login(userData);
        }
    }
}
