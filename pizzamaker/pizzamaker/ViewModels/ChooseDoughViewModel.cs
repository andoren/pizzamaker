using Caliburn.Micro;
using pizzamaker.Models;

namespace pizzamaker.ViewModels
{
    public class ChooseDoughViewModel : Screen
    {
 
        public ChooseDoughViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            var order = Order.getInstance();
            CustomerName = order.Customer.Name;

        }
        private StartUpViewModel mainWindow;
        private string _customerName;

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
        public void LoadNextView()
        {  
             mainWindow.LoadNextView();
        }
        public void LoadPrevView()
        {
            mainWindow.LoadPrevView();
        }
    }
}