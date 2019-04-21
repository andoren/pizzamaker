using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using pizzamaker.Models.Singletons;

namespace pizzamaker.ViewModels
{
    public class StartUpViewModel:Conductor<Screen>
    {


        public StartUpViewModel()
        {
            ActivateItem(sceneFactory.CreateScene(this, 0));

        }
        SceneFactory sceneFactory = new SceneFactory();
        private int _currentLoadedView = 0;

        public int CurrentLoadedView
        {
            get { return _currentLoadedView; }
            set
            {
                _currentLoadedView = value;
                NotifyOfPropertyChange(() => CurrentLoadedView);
            }
        }
        public void LoadNextView() {
            CurrentLoadedView += 1;
            ActivateItem(sceneFactory.CreateScene(this,CurrentLoadedView));
            
           
        }
        public void LoadPrevView() {
            CurrentLoadedView -= 1;
            ActivateItem(sceneFactory.CreateScene(this, CurrentLoadedView));
        }

    }
}
