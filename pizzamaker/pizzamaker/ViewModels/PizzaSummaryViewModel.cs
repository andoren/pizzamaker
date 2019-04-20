using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.ViewModels
{
    class PizzaSummaryViewModel:Screen
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

    }
}
