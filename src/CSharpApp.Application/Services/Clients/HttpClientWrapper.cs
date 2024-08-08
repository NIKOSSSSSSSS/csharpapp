using System.Text.Json;
using System.Text;

namespace CSharpApp.Application.Services.Clients;

public class HttpClientWrapper
    : IHttpClientWrapper
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpClientWrapper> _logger;
    private readonly IConfiguration _configuration;
    public HttpClientWrapper(
        HttpClient httpClient,
        ILogger<HttpClientWrapper> logger,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;

        ConfigureClient();
    }

    public async Task<T> GetAsync<T>(string requestUri)
    {
        var response = await _httpClient.GetAsync(requestUri);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<T> PostAsync<T>(string requestUri, object data)
    {
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(requestUri, content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<T> PutAsync<T>(string requestUri, object data)
    {
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(requestUri, content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task DeleteAsync(string requestUri)
    {
        var response = await _httpClient.DeleteAsync(requestUri);

        response.EnsureSuccessStatusCode();
    }

    private void ConfigureClient()
    {
        string configSection = "BaseUrl";
        string baseUrl = _configuration.GetSection(configSection).Value;

        try
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                _logger.LogError($"Base URL is not set in {configSection}");
                throw new Exception($"Base URL is not set in {configSection}");
            }

            _httpClient.BaseAddress = new Uri(baseUrl);
        }
        catch (UriFormatException)
        {
            _logger.LogError("The format of the Base URL is invalid");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured: {ex.Message}");
        }
    }
}
