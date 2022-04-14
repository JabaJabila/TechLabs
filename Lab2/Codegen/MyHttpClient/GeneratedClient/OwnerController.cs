using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using MyHttpClient.GeneratedModels;

namespace MyHttpClient.GeneratedClient;
public class OwnerController
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
    public OwnerController(string baseUrl)
    {
        _baseUrl = baseUrl;
    }
}