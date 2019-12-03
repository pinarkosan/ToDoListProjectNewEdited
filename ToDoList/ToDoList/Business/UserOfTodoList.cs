using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.Model;

namespace ToDoList.Business
{
   public class UserOfTodoList
    {
        public int UserId { get; set; }
        public int ListId { get; set; }
        public string Namesurname { get; set; }
        public string ListName { get; set; }
        public void AddUserOfListItems(int _ListId, int _UserId)
        {
            try
            {

                using (ToDoListEntities dbEntities = new ToDoListEntities())
                {
                    UsersOfToDoLists todo = new UsersOfToDoLists();
                    todo.ListId = _ListId;
                    todo.UserId = _UserId;
                    dbEntities.UsersOfToDoLists.Add(todo);
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
