using pizzamaker.Models;
using Caliburn.Micro;

namespace pizzamaker.ViewModels
{
    public class SceneFactory
    {
        public SceneFactory()
        {
        }
        public Screen CreateScene(StartUpViewModel mainWindow, int index)
        {
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
                        scene = new PizzaSummaryViewModel(mainWindow);
                        break;
                    }
                case 7:
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