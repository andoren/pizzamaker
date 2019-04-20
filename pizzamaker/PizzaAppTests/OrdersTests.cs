using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pizzamaker.Models;
using pizzamaker.Models.Exceptions;

namespace PizzaAppTests
{

    [TestClass]
    public class OrdersTests
    {
        #region Order class Add method unit tests
        [TestMethod]
        public void AddFoodTestRegular()
        {
            Order order = Order.getInstance();
            Food dough = new Dough();
            bool result = order.Add(dough);
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void AddFoodTestWithNull()
        {
            Order order = Order.getInstance();
            bool result = order.Add(null);
            Assert.AreEqual(result, false);
        }
        [TestMethod]
        public void AddFoodTestWithNegativPrice()
        {
            Order order = Order.getInstance();
            Food dough = new Dough();
            dough.Price = -1.0;
            Exception exception = null;
            try
            {
                bool result = order.Add(dough);
            }
            catch (NegativePriceException e) {
                exception = e;
            }
            
            Assert.AreNotEqual(exception, null);
        }
        [TestMethod]
        public void AddFoodTestWithoutName()
        {
            Order order = Order.getInstance();
            Food dough = new Dough();
            dough.Name = "";
            Exception exception = null;
            try
            {
                bool result = order.Add(dough);
            }
            catch (NoFoodNameException e)
            {
                exception = e;
            }

            Assert.AreNotEqual(exception, null);
        }
        [TestMethod]
        public void AddFoodTestWithoutDescription()
        {
            Order order = Order.getInstance();
            Food dough = new Dough();
            dough.Description = "";
            Exception exception = null;
            try
            {
                bool result = order.Add(dough);
            }
            catch (NoDescriptionException e)
            {
                exception = e;
            }

            Assert.AreNotEqual(exception, null);
        }
        #endregion
        #region Order class Remove method unit tests
        [TestMethod]
        public void RemoveFoodTestRegular()
        {
            Order order = Order.getInstance();
            Food dough = new Dough();
            bool result = order.Remove(dough);
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void RemoveFoodTestWithNull()
        {
            Order order = Order.getInstance();
            Food dough = null;
            bool result = order.Remove(dough);
            Assert.AreEqual(result, false);
        }
        #endregion

        #region Order class overrided toString method unit tests

        [TestMethod]
        public void OneFoodToStringTest()
        {
            Order order = Order.getInstance();
            string actual = order.ToString();
            string expected = "0/1,99/Normal dough/This is a regular dough where we add all the ingridients what is needed to a good pizza dough;";
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void TwoFoodToStringTest()
        {
            Order order = Order.getInstance();
            Food dough = new Dough();
            dough.Id = 1;
            dough.Name = "Regular dough";
            dough.Description = "Its a regular dough. We made it in the traditional way.";
            dough.Price = 1.99;         
            order.Add(dough);
            string actual = order.ToString();
            string expected = "0/1,99/Normal dough/This is a regular dough where we add all the ingridients what is needed to a good pizza dough;1/1,99/Regular dough/Its a regular dough. We made it in the traditional way.;";
            Assert.AreEqual(expected,actual);
        }
        [TestMethod]
        public void EmptyFoodToStringTest()
        {
            Order order = Order.getInstance();
            order.ResetCondiments();
            string actual = order.ToString();
            string expected = "Order is empty!";
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
