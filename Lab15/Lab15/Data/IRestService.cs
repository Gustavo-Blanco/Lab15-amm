using System.Collections.Generic;
using System.Threading.Tasks;
using Lab15.Model;

namespace Lab15.Data
{
    public interface IRestService
    {
        Task<List<TodoItem>> RefreshDataAsync();

        Task SaveTodoItemAsync(TodoItem item, bool isNewItem);
        Task DeleteTodoItemAsync(string id);
    }
}