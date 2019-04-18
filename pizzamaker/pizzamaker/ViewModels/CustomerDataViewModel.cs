using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pizzamaker.Models.Exceptions;
using System.Windows;

namespace pizzamaker.ViewModels
{
    class CustomerDataViewModel:Screen
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

        public string NameTextBox { get; set; } 
        public string EmailTextBox { get; set; } 
        public string MobileTextBox { get; set; } 
        public string AddressTextBox { get; set; }

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
                    mainWindow.LoadNextView();
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
                MessageBox.Show(e.Message);
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
