using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pizzamaker.Models;

namespace PizzaAppTests
{
    /// <summary>
    /// Summary description for CustomersTests
    /// </summary>
    [TestClass]
    public class CustomersTests
    {
       
        [TestMethod]
        public void NameValidationRegularTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobilNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.NameValidation();
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NameValidationWithoutNameTest()
        {
            Customer target = new Customer();
            target.Name = "";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobilNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.NameValidation();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NameValidationWithoutSpaceTest()
        {
            Customer target = new Customer();
            target.Name = "ValamiNév";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobilNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.NameValidation();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NameValidationOnlySpaceTest()
        {
            Customer target = new Customer();
            target.Name = "      ";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobilNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.NameValidation();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NameValidationLessThanSixCharTest()
        {
            Customer target = new Customer();
            target.Name = "less ";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobilNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.NameValidation();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NameValidationMoreThanThirtyCharTest()
        {
            Customer target = new Customer();
            target.Name = "1234567adasdqwokeűkasdűaskdékadÁks adlkűasdkaűdékasűdasdmnfmnkldnadhnpiájőqkwdákasdasádláad";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobilNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.NameValidation();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
    }
}
