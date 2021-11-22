using ClothesDataMicroservice.Model.IRepositories;
using ClothesDataMicroservice.Rest.Commands;
using ClothesDataMicroservice.Model.Entities;
using ClothesDataMicroservice.Logic.Security;

namespace ClothesDataMicroservice.Rest.CommandsHandlers
{
    public class UsersCommandsHandler : ICommandHandler<AddUserCommand>
    {
        private readonly IUsersRepository repository;
        public UsersCommandsHandler(IUsersRepository repository)
        {
            this.repository = repository;
        }
        public void Handle(AddUserCommand command)
        {
            string hash = Sha256Helper.ComputeSha256Hash(command.password);
            repository.AddUser(new User(command.email, hash));
        }
    }
}
