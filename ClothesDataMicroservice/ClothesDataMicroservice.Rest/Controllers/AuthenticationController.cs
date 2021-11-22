using ClothesDataMicroservice.Logic.Security;
using ClothesDataMicroservice.Model.IRepositories;
using ClothesDataMicroservice.Model.Entities;
using System;
using ClothesDataMicroservice.Rest.Application.Dtos;
using ClothesDataMicroservice.Rest.CommandsHandlers;
using ClothesDataMicroservice.Rest.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ClothesDataMicroservice.Rest.Controllers
{

    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenBuilder tokenBuilder;
        private readonly IUsersRepository userRepository;
        private readonly ICommandHandler<AddUserCommand> usersCommandHandler;
        public AuthenticationController(IUsersRepository userRepository, ITokenBuilder tokenBuilder, ICommandHandler<AddUserCommand> usersCommandHandler)
        {
            this.userRepository = userRepository;
            this.tokenBuilder = tokenBuilder;
            this.usersCommandHandler = usersCommandHandler;
        }

        [HttpPost("login")]
        public TokenDto Login([FromBody] UserDataDto passes)
        {
            TokenDto result = new TokenDto();
            User user = userRepository.GetUserByEmail(passes.email);
            if (user == null)
            {
                result.message=String.Format("User with email {0} does'n exist!", passes.email);
                return result;
            }

            if (!user.Hash.Equals(Sha256Helper.ComputeSha256Hash(passes.password)))
            {
                result.message = "Incorrect email or password";
                return result;
            }
            result.token =  tokenBuilder.BuildToken(passes.email);
            return result;
        }

        [HttpPost("register")]
        public TokenDto Register([FromBody] AddUserCommand command)
        {
            TokenDto result = new TokenDto();
            if (userRepository.GetUserByEmail(command.email)!= null)
            {
                result.message = String.Format("User with email {0} have already exist!", command.email);
                return result;
            }
            usersCommandHandler.Handle(command);
            result.token = tokenBuilder.BuildToken(command.email);
            return result; 
        }
    }
}
