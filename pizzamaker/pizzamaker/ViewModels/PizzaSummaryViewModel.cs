using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pizzamaker.ViewModels
{
    public class PizzaSummaryViewModel:Screen
    {
        private StartUpViewModel mainWindow;

        public PizzaSummaryViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Order = Order.getInstance();
        }
        public void LoadPrevView()
        {
            mainWindow.LoadPrevView();
        }
        public void LoadNextView()
        {
            mainWindow.LoadNextView();
        }
        private Order order;
        
        public Order Order
        {
            get { return order; }
            set { order = value; }
            
        }

        public BindableCollection<Food> OrderItems {
            get
            {
                
                return Order.GetItems();
            }
        }
        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
