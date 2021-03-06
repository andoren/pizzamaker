﻿using pizzamaker.Models;
using Caliburn.Micro;
using System;
using pizzamaker.Models.Exceptions;
namespace pizzamaker.ViewModels
{
    public class SceneFactory
    {
        public SceneFactory()
        {
        }
        /// <summary>
        /// Gives back the specific view by index. We need the mainWindow because we use its loadnext and loadprev method to change the view
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Screen CreateScene(StartUpViewModel mainWindow, int index)
        {
            if (mainWindow == null) throw new NullReferenceException();
            else if (index < 0) throw new InvalidIndexExceptionForScene();
            Screen scene = null;
            switch (index)
            {
                case 0: {
                        scene = new StartViewModel(mainWindow);
                        break;
                    }
                case 1: {
                        scene = new CustomerDataViewModel(mainWindow);
                        break;
                    }
                case 2: {
                        scene = new ChooseDoughViewModel(mainWindow);
                        break;
                    }
                case 3: {
                        scene = new ChooseYourSauceViewModel(mainWindow);
                        break;
                    }
                case 4:
                    {
                        scene = new ChooseMeatViewModel(mainWindow);
                        break;
                    }
                case 5:
                    {
                        scene = new SelectToppingsViewModel(mainWindow);
                        break;
                    }
                case 6:
                    {
                        scene = new ChooseCheeseViewModel(mainWindow);
                        break;
                    }
                case 7:
                    {
                        scene = new PizzaSummaryViewModel(mainWindow);
                        break;
                    }
                case 8:
                    {
                        scene = new DeliverViewModel(mainWindow);
                        break;
                    }
                default:
                    break;
            }
            return scene;
        }
    }
}