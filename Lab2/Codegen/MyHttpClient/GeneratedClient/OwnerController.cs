using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using MyHttpClient.GeneratedModels;

namespace MyHttpClient.GeneratedClient;
public class OwnerController
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
    public async Task<OwnerViewModel> CreateOwner(OwnerCreationInfo ownerInfo)
    {
        var query = string.Empty;
        var content = JsonContent.Create(ownerInfo);
        var response = await _httpClient.PostAsync($"http://localhost:8080/owner/create-owner" + $"?{query}", content);
        return JsonSerializer.Deserialize<OwnerViewModel>(await response.Content.ReadAsStringAsync(), _serializerOptions) ?? throw new InvalidOperationException("Request returned null");
    }

    public async Task<List<OwnerViewModel>> GetAll()
    {
        var query = string.Empty;
        var response = await _httpClient.GetAsync($"http://localhost:8080/owner/all" + $"?{query}");
        return JsonSerializer.Deserialize<List<OwnerViewModel>>(await response.Content.ReadAsStringAsync(), _serializerOptions) ?? throw new InvalidOperationException("Request returned null");
    }

    public async Task<OwnerViewModel> GetOwner(long? id)
    {
        var query = string.Empty;
        var response = await _httpClient.GetAsync($"http://localhost:8080/owner/{id}" + $"?{query}");
        return JsonSerializer.Deserialize<OwnerViewModel>(await response.Content.ReadAsStringAsync(), _serializerOptions) ?? throw new InvalidOperationException("Request returned null");
    }

    public async Task DeleteOwner(long? id)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["id"] = string.Join(',', id);
        var response = await _httpClient.DeleteAsync($"http://localhost:8080/owner/delete" + $"?{query}");
    }

    public async Task AddCat(long? ownerId, long? catId)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["catId"] = string.Join(',', catId);
        var content = JsonContent.Create("");
        var response = await _httpClient.PostAsync($"http://localhost:8080/owner/add/{ownerId}" + $"?{query}", content);
    }

    public async Task AddCats(long? ownerId, List<long?> catIds)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["catIds"] = string.Join(',', catIds);
        var content = JsonContent.Create("");
        var response = await _httpClient.PostAsync($"http://localhost:8080/owner/add-many/{ownerId}" + $"?{query}", content);
    }
}