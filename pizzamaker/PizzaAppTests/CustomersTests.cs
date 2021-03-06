﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pizzamaker.Models;
using pizzamaker.Models.Exceptions;

namespace PizzaAppTests
{
    /// <summary>
    /// Summary description for CustomersTests
    /// </summary>
    [TestClass]
    public class CustomersTests
    { //TODO false-ok cseréje excpetionokre
        #region Customer Class Name Validaton
        [TestMethod]
        public void NameValidationRegularTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
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
            target.MobileNumber = "+36700000000";
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
            target.MobileNumber = "+36700000000";
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
            target.MobileNumber = "+36700000000";
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
            target.MobileNumber = "+36700000000";
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
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.NameValidation();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Customer Class Email Validaton
        [TestMethod]
        public void EmailValidationRegularTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EmailValidationEmptyTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EmailValidationContainsSpaceTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ez egykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EmailValidationWithoutAtTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemailgmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EmailValidationLessThan7CharTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "2@a.h";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EmailValidationNoTextBeforeAtTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EmailValidationNoTextAfterAtTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegyemail@";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EmailValidationNoDotTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegy@validemail";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EmailValidationNoTextAfterDotTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegy@validemail.";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.EmailValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region Customer Class Mobile Number Validation
        [TestMethod]
        public void MobileValidationRegularTest() {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.MobileValidaton();
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void MobileValidationEmptyTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.MobileValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void MobileValidationNotStartWithPlusSymbolTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "036700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.MobileValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void MobileValidationNotEqualsCorrectLengthTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+367000000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.MobileValidaton();
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region Customer Class Address Validaton
        [TestMethod]
        public void AddressValidationRegularTest() {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.AddressValidation();
            bool expected = true;
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void AddressValidationEmptyTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "";
            bool actual = target.AddressValidation();
            bool expected = false;
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region Customer Class Mass Validation
        [TestMethod]
        public void MassValidationRegularTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            bool actual = target.MassValidation();
            bool expected = true;
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void MassValidationBadNameTest()
        {
            Customer target = new Customer();
            target.Name = "";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            Exception expected = null;
            try { 
            target.MassValidation();
            }
            catch(CustomerNameErrorException e){
                expected = e;
            }
            Assert.AreNotEqual(expected, null);
        }
        [TestMethod]
        public void MassValidationBadEmailTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "";
            target.MobileNumber = "+36700000000";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            Exception expected = null;
            try
            {
                target.MassValidation();
            }
            catch (CustomerEmailErrorException e)
            {
                expected = e;
            }
            Assert.AreNotEqual(expected, null);
        }
        [TestMethod]
        public void MassValidationBadMobileNumberTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "";
            target.Address = "5540, Szarvas Tessedik Sámuel u. 55.";
            Exception expected = null;
            try
            {
                target.MassValidation();
            }
            catch (CustomerMobileNumberErrorException e)
            {
                expected = e;
            }
            Assert.AreNotEqual(expected, null);
        }
        [TestMethod]
        public void MassValidationBadAddressTest()
        {
            Customer target = new Customer();
            target.Name = "Pekár Mihály";
            target.Email = "ezegykamuemail@gmail.com";
            target.MobileNumber = "+36700000000";
            target.Address = "";
            Exception expected = null;
            try
            {
                target.MassValidation();
            }
            catch (CustomerAddressErrorException e)
            {
                expected = e;
            }
            Assert.AreNotEqual(expected, null);
        }


        #endregion
    }
}
