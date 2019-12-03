using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;
using System.Windows;

namespace ToDoList.Business
{
    public class TodoListItems
    {
        public int ItemId { get; set; }
        public string ListItemName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int ListId { get; set; }
        public string ListName { get; set; }
        public string IsActive { get; set; }
        public bool Status { get; set; }
        public void AddToDoListItems(string _ListItemName,string _Descrption, DateTime _Deadline, int _listId)
        {
            try
            {

                using (ToDoListEntities dbEntities = new ToDoListEntities())
                {
                    ToDoListItems todo = new ToDoListItems();
                    todo.ListItemName = _ListItemName.Trim();
                    todo.Description = _Descrption.Trim();
                    todo.Deadline = _Deadline;
                    todo.ListId = _listId;
                    todo.IsActive = "1";
                    dbEntities.ToDoListItems.Add(todo);
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
