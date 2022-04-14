using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using MyHttpClient.GeneratedModels;

namespace MyHttpClient.GeneratedClient;
public class CatController
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
    public async Task<CatViewModel> CreateCat(CatCreationInfo catInfo)
    {
        var query = string.Empty;
        var content = JsonContent.Create(catInfo);
        var response = await _httpClient.PostAsync($"http://localhost:8080/cat/create-cat" + $"?{query}", content);
        return JsonSerializer.Deserialize<CatViewModel>(await response.Content.ReadAsStringAsync(), _serializerOptions) ?? throw new InvalidOperationException("Request returned null");
    }

    public async Task<List<CatViewModel>> GetAll()
    {
        var query = string.Empty;
        var response = await _httpClient.GetAsync($"http://localhost:8080/cat/all" + $"?{query}");
        return JsonSerializer.Deserialize<List<CatViewModel>>(await response.Content.ReadAsStringAsync(), _serializerOptions) ?? throw new InvalidOperationException("Request returned null");
    }

    public async Task<CatViewModel> GetCat(long? id)
    {
        var query = string.Empty;
        var response = await _httpClient.GetAsync($"http://localhost:8080/cat/{id}" + $"?{query}");
        return JsonSerializer.Deserialize<CatViewModel>(await response.Content.ReadAsStringAsync(), _serializerOptions) ?? throw new InvalidOperationException("Request returned null");
    }

    public async Task DeleteCat(long? id)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["id"] = string.Join(',', id);
        var response = await _httpClient.DeleteAsync($"http://localhost:8080/cat/delete" + $"?{query}");
    }
}