namespace ClothesDataMicroservice.Logic.Security
{
    public interface ITokenBuilder
    {
        string BuildToken(string email);
    }
}
