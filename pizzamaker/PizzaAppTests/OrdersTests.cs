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
    }
}
