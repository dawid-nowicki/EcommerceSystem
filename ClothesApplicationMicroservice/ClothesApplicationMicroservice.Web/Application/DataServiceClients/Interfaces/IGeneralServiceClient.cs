using System.Net.Http;
using System.Threading.Tasks;

namespace ClothesApplicationMicroservice.Web.Application.DataServiceClients
{
    public interface IGeneralServiceClient
    {
        Task<T> GetData<T>(string uri);
        Task<T> GetDataWithAuthorization<T>(string uri, string token);
        Task<HttpResponseMessage> GetHttpResponseMessage(string uri);
        Task<T> PostData<T>(string uri, object content);
    }
}
