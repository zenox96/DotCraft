using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Authlib;

namespace Tests.Authlib
{
    [TestClass]
    public class UserTypeTests
    {
        [TestMethod]
        public void UserTypeByName1()
        {
            Assert.AreEqual(UserTypeExtension.ByName("null"), UserType.NULL);
        }
    }
}
