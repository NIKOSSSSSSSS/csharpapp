namespace CSharpApp.Core.Interfaces;

public interface IHttpClientWrapper
{
    Task<T> GetAsync<T>(string requestUri);
    Task<T> PostAsync<T>(string requestUri, object data);
    Task<T> PutAsync<T>(string requestUri, object data);
    Task DeleteAsync(string requestUri);
}
