using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ToDoList.Business;
using ToDoList.Model;

namespace ToDoList.Facade
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {

        public HomeWindow()
        {

            InitializeComponent();


            using (ToDoListEntities dbEntities = new ToDoListEntities())
            {
                List<TodoLists> todo = dbEntities.TodoLists.Where(x => x.IsActive == "1").ToList();
                if (todo.Count > 0)
                {
                    for (int i = 0; i < todo.Count; i++)
                    {
                        Todolist todoList = new Todolist();
                        todoList.ListName = todo[i].ListName;
                        todoList.ListId = todo[i].ListId;

                        lvToDoList.Items.Add(todoList);

                        DataContext = todo;
                    }

                }

                List<ToDoListItems> todoItem = dbEntities.ToDoListItems.Where(x => x.IsActive == "1").ToList();
                if (todoItem.Count > 0)
                {
                    for (int i = 0; i < todoItem.Count; i++)
                    {
                        TodoListItems todoListItem = new TodoListItems();
                        todoListItem.ItemId = todoItem[i].ItemId;
                        todoListItem.ListItemName = todoItem[i].ListItemName;
                        todoListItem.Description = todoItem[i].Description;
                        todoListItem.Deadline = Convert.ToDateTime(todoItem[i].Deadline);
                        todoListItem.ListId = Convert.ToInt32(todoItem[i].ListId);

                        todoListItem.IsActive = todoItem[i].IsActive;

                        var _listName = (from x in dbEntities.TodoLists
                                         where (x.ListId == todoListItem.ListId)
                                         select x).FirstOrDefault<TodoLists>();
                        if (Convert.ToInt32(todoItem[i].ListId) == 0)
                        {
                            todoListItem.ListName = "";
                        }
                        else todoListItem.ListName = _listName.ListName;


                        if (todoItem[i].Status == "1")
                        {
                            todoListItem.Status = true;
                        }
                        else todoListItem.Status = false;


                        lvToDoListItem.Items.Add(todoListItem);
                        


                    }

                }

                List<UsersOfToDoLists> todoUserOfList = dbEntities.UsersOfToDoLists.ToList();
                if (todoUserOfList.Count > 0)
                {
                    for (int i = 0; i < todoUserOfList.Count; i++)
                    {
                        UserOfTodoList todoUserItem = new UserOfTodoList();
                        todoUserItem.UserId = Convert.ToInt32(todoUserOfList[i].UserId);
                        todoUserItem.ListId = Convert.ToInt32(todoUserOfList[i].ListId);



                        var _listName = (from x in dbEntities.TodoLists
                                         where (x.ListId == todoUserItem.ListId)
                                         select x).FirstOrDefault<TodoLists>();

                        var _userName = (from x in dbEntities.Users
                                         where (x.UserId == todoUserItem.UserId)
                                         select x).FirstOrDefault<Users>();
                        todoUserItem.ListName = _listName.ListName;
                        todoUserItem.Namesurname = _userName.NameSurname;





                        lvUserList.Items.Add(todoUserItem);
                      

                    }

                }

                List<Users> userdb = dbEntities.Users.ToList();
                  if (userdb.Count > 0)
                {
                    for (int i = 0; i < userdb.Count; i++)
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(userdb[i].UserId);
                        user.NameSurname = userdb[i].NameSurname;
                        lvUserList.Items.Add(user);
                        cmbUserName.Items.Add(user);
                    }

                }
            }


        }

        private void HideShow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void AddListName_Click(object sender, RoutedEventArgs e)
        {
            Todolist todo = new Todolist();
            todo.AddToDoList(txtListName.Text);
           Refresh();


        }

        private void DeleteListName_Click(object sender, RoutedEventArgs e)
        {
            ToDoListEntities dbEntities = new ToDoListEntities();
            var selected = lvToDoList.SelectedItems.Cast<Object>().ToArray();
            foreach (var item in selected)
            {
                lvToDoList.Items.Remove(item);
                TodoLists todo = dbEntities.TodoLists.Find(((ToDoList.Business.Todolist)item).ListId);
                todo.IsActive = "0";
                dbEntities.SaveChanges();
            }

        }

        private void cmbtodolist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            ToDoListEntities dbEntities = new ToDoListEntities();
            var selected = lvToDoListItem.SelectedItems.Cast<Object>().ToArray();
            foreach (var item in selected)
            {
                lvToDoListItem.Items.Remove(item);
                ToDoListItems todo = dbEntities.ToDoListItems.Find(((ToDoList.Business.TodoListItems)item).ItemId);
                todo.IsActive = "0";
                dbEntities.SaveChanges();
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            
                TodoListItems todo = new TodoListItems();
                int ListNameId = ((ToDoList.Model.TodoLists)cmbUserofListName.SelectedItem).ListId;
                todo.AddToDoListItems(ListItemName.Text, Descryption.Text, Deadline.SelectedDate.Value, ListNameId);
               
            
           
           Refresh();
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToDoListEntities dbEntities = new ToDoListEntities();
                var selected = lvToDoListItem.SelectedItems.Cast<Object>().ToArray();
                if (selected.Length == 0)
                {
                    MessageBox.Show("Please select the row you want to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                foreach (var item in selected)
                {

                    ToDoListItems todo = dbEntities.ToDoListItems.Find(((ToDoList.Business.TodoListItems)item).ItemId);



                    if (((ToDoList.Business.TodoListItems)selected[0]).Status == true)
                    {
                        todo.Status = "1";
                    }
                    else todo.Status = "0";
                    dbEntities.SaveChanges();
                    MessageBox.Show("Updating Success!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Updating Failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }




        private void AddUserListName_Click(object sender, RoutedEventArgs e)
        {
            int UserNameId = ((ToDoList.Business.User)cmbUserName.SelectedItem).UserId;
            int ListNameId = ((ToDoList.Model.TodoLists)cmbUserofListName.SelectedItem).ListId;
            UserOfTodoList todo = new UserOfTodoList();
            todo.AddUserOfListItems(ListNameId, UserNameId);
           Refresh();
        }
        public void MyFilter(string listName)
        {
            using (ToDoListEntities dbEntities = new ToDoListEntities())
            {

                TodoListItems todoListItem = new TodoListItems();
                lvToDoListItem.Items.Clear();

                var _listName = (from x in dbEntities.ToDoListItems
                                 where (x.ListItemName.Contains(listName))
                                 select x).FirstOrDefault<ToDoListItems>();
                if (_listName != null)
                {
                   
                    todoListItem.ItemId = _listName.ItemId;
                    todoListItem.ListItemName = _listName.ListItemName;
                    todoListItem.Description = _listName.Description;
                    todoListItem.Deadline = Convert.ToDateTime(_listName.Deadline);
                    todoListItem.ListId = Convert.ToInt32(_listName.ListId);

                    todoListItem.IsActive = _listName.IsActive;

                    if (Convert.ToInt32(_listName.ListId) == 0)
                    {
                        todoListItem.ListName = "";
                    }
                    else todoListItem.ListName = _listName.ListItemName;


                    if (_listName.Status == "1")
                    {
                        todoListItem.Status = true;
                    }
                    else todoListItem.Status = false;


                   
                }
                
                lvToDoListItem.Items.Add(todoListItem);
            }

            lvToDoListItem.Items.Refresh();



        }

        private void filter_Click(object sender, RoutedEventArgs e)
        {
            string ListName = txtfilter.Text;
            MyFilter(ListName);
        }
       
      

       void Refresh()
        {
            HomeWindow homeWindow = new HomeWindow();
            this.Hide();
            homeWindow.Show();
            

        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        //private ICollectionView _ListNameCollection;
        
    }
}
