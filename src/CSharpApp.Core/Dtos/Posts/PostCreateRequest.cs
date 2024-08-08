namespace CSharpApp.Core.Dtos.Posts;

    public record PostCreateRequest(
    [property: JsonProperty("title")] string Title,
    [property: JsonProperty("body")] string Body);
