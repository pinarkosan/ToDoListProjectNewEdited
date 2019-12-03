using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoList.Business;

namespace ToDoList.Facade
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {

        #region RegisterInfo
        public bool isEqual = true;
        public string namesurname { get; set; }
        public string userName { get; set; }
        public string confirm { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        #endregion
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private bool GetRegisterInfo()
        {
            bool result = true;
            isEqual = true;
            namesurname = Namesurname.Text;
            userName = Username.Text;
            password = Password.Password;
            confirm = Confirm.Password;
            email = Email.Text;
            if (namesurname == "" || userName == "" || Password.Password == "" || Confirm.Password == "" || Email.Text == "")
            {

                result = false;
            }
            if (password != confirm)
            {
                isEqual = false;
            }

            return result;

        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool Isfull = GetRegisterInfo();
            if (Isfull)
            {
                if (isEqual)
                {
                    Register register = Register.GetInstance();
                    register.CreateUserAccount(namesurname, userName, password, email);
                }
                else MessageBox.Show("The passwords you entered do not match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else MessageBox.Show("Please fill the all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();

        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?",
                                         "Confirmation",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Minimized_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
