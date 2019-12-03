using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.Model;

namespace ToDoList.Business
{
    public class Todolist
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
        public char IsActive { get; set; }

        public void AddToDoList(string ListName)
        {
            try
            {

                using (ToDoListEntities dbEntities = new ToDoListEntities())
                {
                    TodoLists todo = new TodoLists();
                    todo.ListName = ListName.Trim();
                    todo.IsActive = "1";
                    dbEntities.TodoLists.Add(todo);
                    dbEntities.SaveChanges();

                    MessageBox.Show("Adding Success!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Adding Failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
