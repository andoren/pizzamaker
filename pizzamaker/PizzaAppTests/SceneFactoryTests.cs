using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pizzamaker.Models;
using pizzamaker.Models.Factories;
using pizzamaker.Models.Exceptions;
using pizzamaker.ViewModels;
using Caliburn.Micro;

namespace PizzaAppTests
{
    [TestClass]
    public class SceneFactoryTests
    {
        [TestMethod]
        public void GetStartViewModel()
        {
            SceneFactory target = new SceneFactory();
            Screen actual = target.CreateScene(new StartUpViewModel(), 0);
            Assert.IsTrue(actual is StartViewModel);
        }
        [TestMethod]
        public void GetInformationViewModel()
        {
            SceneFactory target = new SceneFactory();
            Screen actual = target.CreateScene(new StartUpViewModel(), 1);
            Assert.IsTrue(actual is CustomerDataViewModel);
        }
        [TestMethod]
        public void GetDoughViewModel()
        {
            SceneFactory target = new SceneFactory();
            Screen actual = target.CreateScene(new StartUpViewModel(), 2);
            Assert.IsTrue(actual is ChooseDoughViewModel);
        }
        [TestMethod]
        public void GetSauceViewModel()
        {
            SceneFactory target = new SceneFactory();
            Screen actual = target.CreateScene(new StartUpViewModel(), 3);
            Assert.IsTrue(actual is ChooseYourSauceViewModel);
        }
        [TestMethod]
        public void GetMeatViewModel()
        {
            SceneFactory target = new SceneFactory();
            Screen actual = target.CreateScene(new StartUpViewModel(), 4);
            Assert.IsTrue(actual is ChooseMeatViewModel);
        }
        [TestMethod]
        public void GetToppingViewModel()
        {
            SceneFactory target = new SceneFactory();
            Screen actual = target.CreateScene(new StartUpViewModel(), 5);
            Assert.IsTrue(actual is SelectToppingsViewModel);
        }
        [TestMethod]
        public void GetCheeseViewModel()
        {
            SceneFactory target = new SceneFactory();
            Screen actual = target.CreateScene(new StartUpViewModel(), 6);
            Assert.IsTrue(actual is ChooseCheeseViewModel);
        }
        [TestMethod]
        public void GetSummaryViewModel()
        {
            SceneFactory target = new SceneFactory();
            Screen actual = target.CreateScene(new StartUpViewModel(), 7);
            Assert.IsTrue(actual is PizzaSummaryViewModel);
        }

        [TestMethod]
        public void MainWindowIsNull()
        {
            SceneFactory target = new SceneFactory();
            Exception actual = null;
            try
            {
                Screen screen = target.CreateScene(null, 7);
            }
            catch (NullReferenceException e) {
                actual = e;
            }
            Assert.IsNotNull(actual);
        }
        [TestMethod]
        public void NegativIndex()
        {
            SceneFactory target = new SceneFactory();
            Exception actual = null;
            try
            {
                Screen screen = target.CreateScene(new StartUpViewModel(), -1);
            }
            catch (InvalidIndexExceptionForScene e)
            {
                actual = e;
            }
            Assert.IsNotNull(actual);
        }
    }
}
