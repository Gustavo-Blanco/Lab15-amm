using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab15.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab15.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoItemPage : ContentPage
    {
        private bool _isNewItem;
        public TodoItemPage(bool isNewItem = false)
        {
            InitializeComponent();
            _isNewItem = isNewItem;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.TodoManager.SaveTaskAsync(todoItem, _isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.TodoManager.DeleteTaskAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}