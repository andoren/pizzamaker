using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;


namespace pizzamaker.ViewModels
{
    public class StartUpViewModel:Conductor<Screen>
    {


        public StartUpViewModel()
        {
            ActivateItem(new StartViewModel(this));
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

            ActivateItem(new CustomerDataViewModel());
            CurrentLoadedView += 1;
            NotifyOfPropertyChange(() => ButtonsIsVisible);
        }
        public void LoadPrevView() {
            CurrentLoadedView -= 1;
            ActivateItem(new CustomerDataViewModel());
        }
        public bool ButtonsIsVisible
        {
            get {
                return CurrentLoadedView != 0 ? true : false;
            }

        }
    }
}
