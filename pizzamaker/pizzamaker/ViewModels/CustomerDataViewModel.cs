using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.ViewModels
{
    class CustomerDataViewModel:Screen
    {
        public CustomerDataViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        StartUpViewModel mainWindow;
        public string NameError { get; set; } 
        public string EmailError { get; set; } 
        public string MobilError { get; set; } 
        public string AddressError { get; set; }

        public string NameTextBox { get; set; } 
        public string EmailTextBox { get; set; } 
        public string MobileTextBox { get; set; } 
        public string AddressTextBox { get; set; }

        public void LoadNextView() {
            Customer customer = new Customer();

            mainWindow.LoadNextView();
        }

    }
}
