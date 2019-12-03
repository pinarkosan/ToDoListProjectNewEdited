using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Facade;
using ToDoList.Model;
using System.Windows;

namespace ToDoList.Business
{
    public class Register : IEncrypting
    {

        public string Namesurname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        private static Register _instance;
        private Register()
        {

        }
        public static Register GetInstance()
        {
            if (_instance == null)
                _instance = new Register();
            return _instance;

        }

        public void CreateUserAccount(string _Namesurname, string _Username, string _Password, string _Email)
        {

          
            string encryptingPassword = Encrypting(_Password).Trim();
            try
            {
               
                using (ToDoListEntities dbEntities = new ToDoListEntities())
                {
                    var UserNamedb = (from x in dbEntities.Users
                                     where (x.Username == _Username)
                                     select x).FirstOrDefault<Users>();
                    if (UserNamedb == null)
                    {
                        Users user = new Users();
                        user.NameSurname = _Namesurname.Trim();
                        user.Username = _Username.Trim();
                        user.Password = encryptingPassword;
                        user.Email = _Email.Trim();
                        dbEntities.Users.Add(user);
                        dbEntities.SaveChanges();

                        MessageBox.Show("Adding Success!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("This user name is already exists!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                   
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Adding Failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        string hash = Cons.hash;
        public string Encrypting(string password)
        {
            byte[] result;
            if (password == null)
            {
                password = String.Empty;
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            using (MD5CryptoServiceProvider mds = new MD5CryptoServiceProvider())
            {
                byte[] keys = mds.ComputeHash(UTF32Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    result = transform.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

                }

            }


            return Convert.ToBase64String(result, 0, result.Length);
        }

    }
}
