using CSharpApp.Core.Dtos.Todos;

namespace CSharpApp.Core.Interfaces;

public interface ITodoService
{
    Task<TodoRecord?> GetTodoByIdAsync(int id);
    Task<IEnumerable<TodoRecord>> GetAllTodosAsync();
}