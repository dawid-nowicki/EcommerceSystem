namespace ClothesDataMicroservice.Rest.Commands
{
    public class AddUserCommand : ICommand
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
