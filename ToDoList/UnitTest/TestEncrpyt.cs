using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Business;

namespace UnitTest
{
    [TestClass]
    public class TestEncrpyt
    {
        private const string password = "123";
        [TestMethod]
        public void Encrypting()
        {
            Register register = Register.GetInstance();
            string result = register.Encrypting(password);

            //Expected value "IfBtfkVUTYHuZNVZkS5I1A=="
            Assert.AreEqual("prtXzGopiw4=", result);
        }
    }
}
