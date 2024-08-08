using CSharpApp.Core.Dtos.Posts;

namespace CSharpApp.Core.Interfaces;

public interface IPostService
{
    Task<PostRecord> GetPostByIdAsync(int id);
    Task<IEnumerable<PostRecord>> GetAllPostsAsync();
    Task<PostRecord> CreatePostAsync(PostCreateRequest request);
    Task<PostRecord> UpdatePostAsync(int postId, PostUpdateRequest request);
    Task DeletePostAsync(int postId);
}
