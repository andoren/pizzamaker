using Caliburn.Micro;
using pizzamaker.Models;
using pizzamaker.Models.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace pizzamaker.ViewModels
{
    class DeliverViewModel : Screen
    {
        private StartUpViewModel mainWindow;

        public DeliverViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Task.Factory.StartNew(Prepare);
        }

        private void InQueue() {
            for (int i = 5; i >= 0; i--)
            {
                
                Preparing = "Your order is in queue(" + i + ")";
                Thread.Sleep(1000);

            }
        }
        private void Kneading() {

            for (int i = 5; i >= 0; i--)
            {

                Preparing = "Kneading your "+Order.Dough.Name+"(" + i + ")";
                Thread.Sleep(1000);

            }
        }
        private void ApplyingSauce() {
            
            for (int i = 2; i >= 0; i--)
            {

                Preparing = "Applying your "+Order.Sauce.Name+"(" + i + ")";
                Thread.Sleep(1000);

            }
        }
        private void AddingMeat() {
            for (int i = 2; i >= 0; i--)
            {

                Preparing = "Adding "+Order.Meat.Name+"...(" + i + ")";
                Thread.Sleep(1000);

            }
        }
        private void AddingToppings() {
            foreach (var item in Order.AllToppings.AllToppings)
            {
                for (int i = 1; i >=0; i--)
                {
                    Preparing = "Adding " + item.Name + "(" + i + ")";
                    Thread.Sleep(1000);
                }

            }



            }
        
        private void AddingCheese() {
            for (int i = 1; i >= 0; i--)
            {

                Preparing = "Sprinkling  "+Order.Cheese.Name+"(" + i + ")";
                Thread.Sleep(1000);

            }
        }
        private void Boxing() {

            for (int i = 2; i >= 0; i--)
            {

                Preparing = "Boxing(" + i + ")";
                Thread.Sleep(1000);

            }
        }
        
        private void Delivery() {
            Preparing = "Your order is on the way ;)";
            Delivering = System.IO.Directory.GetCurrentDirectory() + @"\imgs\delivery.gif";
            Thread.Sleep(5000);
            Preparing = "Bye, Bye!!!";
            Thread.Sleep(2000);
            
        }

        private void Prepare() {
            InQueue();
            Kneading();
            ApplyingSauce();
            AddingMeat();
            AddingToppings();
            AddingCheese();
            Boxing();
            Delivery();
        }
        private string _prepareing;
        public void Shutdown() {
            Application.Current.Shutdown();
        }

        public string Preparing
        {
            get { return _prepareing; }
            set {
                _prepareing = value;
                NotifyOfPropertyChange(() => Preparing);
            }
        }

        public Order Order
        {
            get {
                return Order.getInstance();
            }
        }
        private string _delivering; 
        public string Delivering {
            get {
                return _delivering;
            }
            set {
                _delivering = value;
                
                NotifyOfPropertyChange(() => Delivering);

            }
        }
        public string Greetings
        {
            get {
                return string.Format("Congratulation {0}. You just orderd a pizza from Misi's pizzafactory!", Order.Customer.Name);
            }
           
        }     

    }
}
