using System;
using pizzamaker.Models.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pizzamaker.Models;
using pizzamaker.Models.Foods;
using pizzamaker.Models.Exceptions;

namespace PizzaAppTests
{
    [TestClass]
    public class FoodFactoryTests
    {
        [TestMethod]
        public void GetDough()
        {
            FoodFactory target = new FoodFactory(); 
            Food actual = target.GetFood("dough");
            Assert.IsTrue(actual is Dough);
        }
        [TestMethod]
        public void GetSauce()
        {
            FoodFactory target = new FoodFactory();
            Food actual = target.GetFood("sauce");
            Assert.IsTrue(actual is Sauce);
        }
        [TestMethod]
        public void GetMeat()
        {
            FoodFactory target = new FoodFactory();
            Food actual = target.GetFood("meat");
            Assert.IsTrue(actual is Meat);
        }
        [TestMethod]
        public void GetTopping()
        {
            FoodFactory target = new FoodFactory();
            Food actual = target.GetFood("topping");
            Assert.IsTrue(actual is Topping);
        }
        [TestMethod]
        public void GetCheese()
        {
            FoodFactory target = new FoodFactory();
            Food actual = target.GetFood("cheese");
            Assert.IsTrue(actual is Cheese);
        }
        [TestMethod]
        public void GetFoodByNull()
        {
            FoodFactory target = new FoodFactory();

            Exception actual = null;
            try
            {
                Food food = target.GetFood(null);
            }
            catch (NullReferenceException e) {
                actual = e;
            }
            Assert.IsNotNull(actual);
        }
        [TestMethod]
        public void GetFoodByInvalidLength()
        {
            FoodFactory target = new FoodFactory();

            Exception actual = null;
            try
            {
                Food food = target.GetFood("3214143213123123213133");
            }
            catch (InvalidLegthExceptionForFood e)
            {
                actual = e;
            }
            Assert.IsNotNull(actual);
        }
    }
}
