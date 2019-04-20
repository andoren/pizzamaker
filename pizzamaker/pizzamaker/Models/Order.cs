using Caliburn.Micro;
using pizzamaker.Models.Exceptions;
using pizzamaker.Models.Foods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models
{
    public class Order:Food, INotifyPropertyChangedEx
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
        private Food[] foods = new Food[5];

        public BindableCollection<Food> GetItems()
        {
            BindableCollection<Food> temp = new BindableCollection<Food>();
            Food summaryfood = new SummaryFood();
            summaryfood.Name = "";
            summaryfood.Description = "Your order price is: ";
            foreach (Food item in foods)
            {
                if (!(item is Toppings)) {
                    temp.Add(item);
                    summaryfood.Price += item.Price;
                } 
            }
            foreach (Food item in AllToppings.AllToppings)
            {
                temp.Add(item);
                summaryfood.Price += item.Price;
            }
            temp.Add(summaryfood);
            return temp;
        }

        private BindableCollection<Food> pizzaCondiments;
        public BindableCollection<Food> PizzaCondiments {
            get {
                BindableCollection<Food> temp = new BindableCollection<Food>();
                foreach (Food item in foods)
                {
                    temp.Add(item);
                }
                
                return temp;
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

        private Toppings _toppings;
        public Toppings AllToppings
        {
            get {
                
                return _toppings;
                

            }
            set {
                _toppings = value;
            }
        }
        private Cheese cheese;

        public Cheese Cheese
        {
            get { return cheese; }
            set { cheese = value; }
        }

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        #endregion

        public override double Price {
            get
            {
                double fullprice = 0;
                foreach (Food food in foods)
                {
                    if(food !=null)fullprice += food.Price;
                }
                
                return fullprice;
            }
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
                    NotifyOfPropertyChange("Price");
                    return true;
                }
            }
            return false;
        }
        public bool AddAt(Food food, int index) {
            if (food != null)
            {
                foods[index] = food;
                NotifyOfPropertyChange("Price");
                NotifyOfPropertyChange("PizzaCondiments");
                return true;
            }
            return false;
        }
        public bool Remove(Food food) {
            if (food != null) {
                pizzaCondiments.Remove(food);
                NotifyOfPropertyChange("Price");
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

        #region NotifyPropertyChangedInterface Properties etc...
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsNotifying { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public void NotifyOfPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void Refresh()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
