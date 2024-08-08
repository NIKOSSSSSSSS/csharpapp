using CSharpApp.Core.Dtos.Posts;
using CSharpApp.Core.Dtos.Todos;

namespace CSharpApp.Application.Services;

public class TodoService : ITodoService
{
    private readonly ILogger<TodoService> _logger;
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly IConfiguration _configuration;

    public TodoService(ILogger<TodoService> logger,
        IConfiguration configuration,
        IHttpClientWrapper httpClientWrapper)
    {
        _logger = logger;
        _httpClientWrapper = httpClientWrapper;
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<IEnumerable<TodoRecord>> GetAllTodosAsync()
    {
        return await _httpClientWrapper.GetAsync<List<TodoRecord>>("todos");
    }

    public async Task<TodoRecord> GetTodoByIdAsync(int id)
    {
        return await _httpClientWrapper.GetAsync<TodoRecord>($"todos/{id}");
    }
}