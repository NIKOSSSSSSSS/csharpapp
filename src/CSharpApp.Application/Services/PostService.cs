using CSharpApp.Core.Dtos.Posts;

namespace CSharpApp.Application.Services;

public class PostService : IPostService
{
    private readonly ILogger<TodoService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientWrapper _httpClientWrapper;

    public PostService(ILogger<TodoService> logger,
         IConfiguration configuration,
         IHttpClientWrapper httpClientWrapper)
    {
        _logger = logger;
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _httpClientWrapper = httpClientWrapper;
    }


    public async Task<IEnumerable<PostRecord>> GetAllPostsAsync()
    {
        return await _httpClientWrapper.GetAsync<List<PostRecord>>("posts");
    }

    public async Task<PostRecord> GetPostByIdAsync(int id)
    {
        return await _httpClientWrapper.GetAsync<PostRecord>($"posts/{id}");
    }

    public async Task<PostRecord> CreatePostAsync(PostCreateRequest request)
    {
        return await _httpClientWrapper.PostAsync<PostRecord>("posts", request);
    }

    public async Task<PostRecord> UpdatePostAsync(int postId, PostUpdateRequest request)
    {
        return await _httpClientWrapper.PutAsync<PostRecord>($"posts/{postId}", request);
    }

    public async Task DeletePostAsync(int postId)
    {
        await _httpClientWrapper.DeleteAsync($"posts/{postId}");
    }
}