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
                default:
                    break;
            }
            return scene;
        }
    }
}