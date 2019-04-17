using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.ViewModels
{
    class StartViewModel:Screen
    {

        public StartViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        private StartUpViewModel mainWindow;
        public void Start() {
            mainWindow.LoadNextView();
        }
    }
}
