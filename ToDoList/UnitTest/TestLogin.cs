using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class TestLogin
    {

        private  bool isUser = false;
        private const string userName = "pinar";
        private const string password = "123";
        [TestMethod]
        public void Login()
        {
            if (userName == "pinar" && password == "123")
            {
                isUser = true;
            }

            //Expected value "123"
            Assert.AreEqual(true, isUser);

        }

    }
}
