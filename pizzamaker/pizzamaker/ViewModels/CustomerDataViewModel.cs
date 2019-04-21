using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pizzamaker.Models.Exceptions;
using System.Windows;
using pizzamaker.Models.Singletons;

namespace pizzamaker.ViewModels
{
    public class CustomerDataViewModel:Screen
    {
        public CustomerDataViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            LoadDataIfExits();
        }
        StartUpViewModel mainWindow;
        private string _nameError;
        public string NameError
        {
            get { return _nameError; }
            set {
                _nameError = value;
                NotifyOfPropertyChange(() => NameError);
            }
        }
        private string _emailError;
        public string EmailError
        {
            get { return _emailError; }
            set
            {
                _emailError = value;
                NotifyOfPropertyChange(()=>EmailError);
            }
        }
        private string _mobilError;
        public string MobilError {
            get {
                return _mobilError;
            }
            set {
                _mobilError = value;
                NotifyOfPropertyChange(() => MobilError);
            }
        }
        private string _addressError;
        public string AddressError {
            get{
                return _addressError;
            }
            set {
                _addressError = value;
                NotifyOfPropertyChange(() => AddressError);
            }
        }

        public string NameTextBox { get; set; } = "Pekár Mihály";
        public string EmailTextBox { get; set; } = "mpekar55@gmail.com";
        public string MobileTextBox { get; set; } = "+36707016759";
        public string AddressTextBox { get; set; } = "5540 Szarvas, Tessedik Sámuel utca 55.";

        private void ResetErrorsTexts() {
            NameError = "";
            EmailError = ""; 
            MobilError = "";
            AddressError = "";
        }

        public void LoadNextView() {
            Customer customer = new Customer();
            customer.Name = NameTextBox;
            customer.Email = EmailTextBox;
            customer.MobileNumber = MobileTextBox;
            customer.Address = AddressTextBox;
            try {
                ResetErrorsTexts();
                if (customer.MassValidation()) {
                    var order = Order.getInstance();
                    order.Customer = customer;
                    try
                    {
                        mainWindow.LoadNextView();
                    }
                    catch (Exception e) {
                        var logger = LogHelper.getInstance();
                        logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadNextView", e.Message);
                    }
                } 
            }
            catch (CustomerNameErrorException)
            {
                NameError = "Invalid name. eg.: John Smith";
            }
            catch (CustomerEmailErrorException)
            {
                EmailError = "Invalid email. eg.: example@example.com";
            }
            catch (CustomerMobileNumberErrorException)
            {
                MobilError = "Invalid mobile number. eg.: +36701234567";
            }
            catch (CustomerAddressErrorException)
            {
                AddressError = "Invalid address. eg.: 3300 Eger, Leányka utca 2.";
            }
            catch (Exception e)
            {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadNextView", e.Message);
            }

        }

        private void LoadDataIfExits()
        {
            var order = Order.getInstance();
            if (order.Customer != null) {
                NameTextBox = order.Customer.Name;
                EmailTextBox = order.Customer.Email;
                MobileTextBox = order.Customer.MobileNumber;
                AddressTextBox = order.Customer.Address;
            }

        }
    }
}
