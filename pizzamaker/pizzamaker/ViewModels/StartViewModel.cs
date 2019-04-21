using Caliburn.Micro;
using pizzamaker.Models.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pizzamaker.ViewModels
{
    public class StartViewModel:Screen
    {

        public StartViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        private StartUpViewModel mainWindow;
        public void Start() {
            try
            {
                mainWindow.LoadNextView();
            }
            catch (Exception e) {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadNextView", e.Message);
            }
        }
        public void Exit() {
            Application.Current.Shutdown();
        }
    }
}
