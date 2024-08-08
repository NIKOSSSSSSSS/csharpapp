using CSharpApp.Application.Services;
using CSharpApp.Application.Services.Clients;
using CSharpApp.Core.Dtos.Posts;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger());

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Add services to the container.
builder.Services.AddHttpClient<IHttpClientWrapper,HttpClientWrapper>();
builder.Services.AddScoped<ITodoService,TodoService>();
builder.Services.AddScoped<IPostService,PostService>();

//builder.Services.AddHttpClient
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/todos", async (ITodoService todoService) =>
    {
        var todos = await todoService.GetAllTodosAsync();
        return todos;
    })
    .WithName("GetTodos")
    .WithOpenApi();

app.MapGet("/posts", async (IPostService postService) =>
{
    var posts = await postService.GetAllPostsAsync();
    return posts;
})
    .WithName("GetPosts")
    .WithOpenApi();

app.MapGet("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
{
    var todos = await postService.GetPostByIdAsync(id);
    return todos;
})
    .WithName("GetPostById")
    .WithOpenApi();

app.MapPost("/posts", async (IPostService postService, PostCreateRequest postCreateRequest) =>
{
    PostRecord post = await postService.CreatePostAsync(postCreateRequest);
    return Results.Created($"/posts/{post.Id}", post);
})
    .WithName("CreatePost")
    .WithOpenApi();

app.MapDelete("/posts/{id}", async ([FromRoute] int id, IPostService postService) =>
{
    await postService.DeletePostAsync(id);
    return Results.NoContent();
})
    .WithName("DeletePostById")
    .WithOpenApi();

app.MapGet("/todos/{id}", async ([FromRoute] int id, ITodoService todoService) =>
    {
        var todos = await todoService.GetTodoByIdAsync(id);
        return todos;
    })
    .WithName("GetTodosById")
    .WithOpenApi();

app.Run();