using System.Collections.Generic;
using System.Threading.Tasks;
using Lab15.Model;

namespace Lab15.Data
{
    public class TodoItemManager
    {
        private IRestService _restService;

        public TodoItemManager(IRestService service)
        {
            _restService = service;
        }

        public Task<List<TodoItem>> GetTasksAsync()
        {
            return _restService.RefreshDataAsync();
        }
        
        public Task SaveTaskAsync (TodoItem item, bool isNewItem = false)
        {
            return _restService.SaveTodoItemAsync(item, isNewItem);
        }
        
        public Task DeleteTaskAsync (TodoItem item)
        {
            return _restService.DeleteTodoItemAsync(item.ID);
        }
    }
}