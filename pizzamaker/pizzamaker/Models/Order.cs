using Caliburn.Micro;
using pizzamaker.Models.Exceptions;
using pizzamaker.Models.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models
{
    public class Order:Food
    {
        private Order()
        {
            pizzaCondiments = new BindableCollection<Food>();
        }
        static Order order = null;

        public static Order getInstance() {
            if (order == null) {
                lock (typeof(Order)) {
                    if (order == null) order = new Order();
                }
                
            }
            return order;
        }

        private BindableCollection<Food> pizzaCondiments;
        public BindableCollection<Food> PizzaCondiments {
            get {
                return pizzaCondiments;
            }
        }
        #region Pizza condiments 
        private Customer customer;
        private Dough dough;
        public Dough Dough
        {
            get { return dough; }
            set { dough = value; }
        }

        private Sauce _sauce;
        public Sauce Sauce
        {
            get { return _sauce; }
            set { _sauce = value; }
        }

        private Meat meat;
        public Meat Meat
        {
            get { return meat; }
            set { meat = value; }
        }

        private Topping[] _toppings;
        public Topping[] Toppings
        {
            get { return _toppings; }
            set { _toppings = value; }
        }
        #endregion


        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public bool Add(Food food)
        {
            if (food != null ) {
                if (food.Price < 0)
                {
                    throw new NegativePriceException();
                }
                else if (food.Name == "")
                {
                    throw new NoFoodNameException();
                }
                else if (food.Description == "")
                {
                    throw new NoDescriptionException();
                }
                else {
                    pizzaCondiments.Add(food);
                    return true;
                }
            }
            return false;
        }
        public bool Remove(Food food) {
            if (food != null) {
                pizzaCondiments.Remove(food);
                return true;
            }
            return false;
        }
        public void ResetCondiments() {
            pizzaCondiments = new BindableCollection<Food>();
        }
        public override string ToString()
        {
            if (pizzaCondiments.Count == 0) return "Order is empty!";
            string mydata = string.Empty;
            foreach (Food item in pizzaCondiments)
            {
                string fooddata = string.Format("{0}/{1}/{2}/{3};",item.Id,item.Price,item.Name,item.Description);
                mydata += fooddata;
            }
            return mydata;
        }

    }
}
