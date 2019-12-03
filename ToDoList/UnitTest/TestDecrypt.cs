using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Business;

namespace UnitTest
{
    [TestClass]
    public class TestDecrypt
    {
        private const string password = "prtXzGopiw4=";
        [TestMethod]
        public void Decrypting()
        {
            Login login = new Login();
            string result = login.Decrypting(password);

            //Expected value "123"
            Assert.AreEqual("123", result);

        }
    }
}
