using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Facade;
using System.Security.Cryptography;
using System.IO;
using ToDoList.Model;

namespace ToDoList.Business
{
    public class Login : IDecrypting
    {
        string hash = Cons.hash;
        public string Decrypting(string password)
        {
            byte[] result;
            if (password == null)
            {
                password = String.Empty;
            }

            byte[] passwordBytes = Convert.FromBase64String(password);
            using (MD5CryptoServiceProvider mds = new MD5CryptoServiceProvider())
            {
                byte[] keys = mds.ComputeHash(UTF32Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    result = transform.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

                }

            }


            return UTF8Encoding.UTF8.GetString(result);
        }
        public void login(string Username, string Password)
        {
            HomeWindow home = new HomeWindow();
            MainWindow mainWindow = new MainWindow();
            try
            {
                Register register = Register.GetInstance();
                string encryptingPassword = register.Encrypting(Password);
                using (var dbEntities = new ToDoListEntities())
                {
                    var _username = (from x in dbEntities.Users
                                     where x.Username == Username
                                     select x).FirstOrDefault<Users>();

                    var _password = (from x in dbEntities.Users
                                     where x.Password == encryptingPassword
                                     select x).FirstOrDefault<Users>();

                  
                    
                    if (_username != null && _password !=null)
                    {
                        CloseAllWindows();
                        home.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username or Password incorrect!", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            

        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Hide();
        }
    }
}
