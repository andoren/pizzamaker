using System;
using System.Text;
using System.Collections.Generic;
using pizzamaker.Models.Utilities;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pizzamaker.Models;
using pizzamaker.Models.Foods;
using pizzamaker.Models.Singletons;

namespace PizzaAppTests
{
    /// <summary>
    /// Summary description for DatabaseHelper
    /// </summary>
    [TestClass]
    public class DatabaseHelperTests
    {


        [TestMethod]
        public void GetDoughs()
        {
            var target =  DatabaseHelper.getInstance();
            BindableCollection <Food> cls  = target.GetFoodsByType("dough");
            bool excepted = true;
            bool actual = true;
            foreach (var item in cls)
            {
                if (!(item is Dough)) actual = false;
            }
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        public void GetSauces()
        {
            var target = DatabaseHelper.getInstance();
            BindableCollection<Food> cls = target.GetFoodsByType("sauce");
            bool excepted = true;
            bool actual = true;
            foreach (var item in cls)
            {
                if (!(item is Sauce)) actual = false;
            }
            Assert.AreEqual(excepted, actual);
        }
        [TestMethod]
        public void GetMeats()
        {
            var target = DatabaseHelper.getInstance();
            BindableCollection<Food> cls = target.GetFoodsByType("meat");
            bool excepted = true;
            bool actual = true;
            foreach (var item in cls)
            {
                if (!(item is Meat)) actual = false;
            }
            Assert.AreEqual(excepted, actual);
        }
        [TestMethod]
        public void GetToppings()
        {
            var target = DatabaseHelper.getInstance();
            BindableCollection<Food> cls = target.GetFoodsByType("topping");
            bool excepted = true;
            bool actual = true;
            foreach (var item in cls)
            {
                if (!(item is Topping)) actual = false;
            }
            Assert.AreEqual(excepted, actual);
        }
        [TestMethod]
        public void GetCheese()
        {
            var target = DatabaseHelper.getInstance();
            BindableCollection<Food> cls = target.GetFoodsByType("cheese");
            bool excepted = true;
            bool actual = true;
            foreach (var item in cls)
            {
                if (!(item is Cheese)) actual = false;
            }
            Assert.AreEqual(excepted, actual);
        }
    }
}
