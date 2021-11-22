using ClothesDataMicroservice.Rest.Commands;

namespace ClothesDataMicroservice.Rest.CommandsHandlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        void Handle(T command);
    }
}
