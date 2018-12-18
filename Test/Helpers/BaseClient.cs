using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Model;

namespace Test.Helpers
{
    internal class BaseClient
    {
        private readonly string _secretKey;
        internal BaseClient(string secretKey)
        {
            _secretKey = secretKey;
        }
        internal async Task<ResponseModel> PostEntities(string urlLink, string content)
        {
            HttpClient httpClient = HttpConnection.CreateClient(_secretKey);
            HttpResponseMessage httpResponse = await httpClient.PostAsync(urlLink, new StringContent(content, Encoding.UTF8, Constants.ContentTypeHeaderJson));
            ResponseModel response = new ResponseModel
            {
                Data = await httpResponse.Content.ReadAsStringAsync(),
                Response = httpResponse
            };
            return response;
        }
        internal async Task<ResponseModel> GetEntities(string urlLink)
        {
            HttpClient client = HttpConnection.CreateClient(_secretKey);
            Task<HttpResponseMessage> response = client.GetAsync(urlLink);
            string content = await response.Result.Content.ReadAsStringAsync();
            ResponseModel response_model = new ResponseModel
            {
                Data = content,
                Response = await response
            };
            return response_model;
        }
        internal async Task<ResponseModel> PutEntities(string urlLink, string content)
        {
            HttpClient client = HttpConnection.CreateClient(_secretKey);
            HttpResponseMessage response = await client.PutAsync(urlLink, new StringContent(content, Encoding.UTF8, Constants.ContentTypeHeaderJson));
            ResponseModel response_model = new ResponseModel
            {
                Data = await response.Content.ReadAsStringAsync(),
                Response = response
            };
            return response_model;
        }

        internal async Task<ResponseModel> DeleteEntities(string urlLink)
        {
            HttpClient client = HttpConnection.CreateClient(_secretKey);
            HttpResponseMessage response = await client.DeleteAsync(urlLink);
            string content = await response.Content.ReadAsStringAsync();
            ResponseModel response_model = new ResponseModel
            {
                Data = content,
                Response = response
            };
            return response_model;
        }

    }
}
