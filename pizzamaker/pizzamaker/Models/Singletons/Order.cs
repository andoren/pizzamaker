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
    public class Order:Food 
    {
        private Order()
        {
            pizzaCondiments = new BindableCollection<Food>();
        }
        static Order order = null;
        /// <summary>
        /// Gives back the order singleton if it was not called yet creat an instance.
        /// </summary>
        /// <returns></returns>
        public static Order getInstance() {
            if (order == null) {
                lock (typeof(Order)) {
                    if (order == null) order = new Order();
                }
                
            }
            return order;
        }
        //We store here the choosen foods
        private Food[] foods = new Food[5];
        //We need this at the summaryview, its calculate the massprice
        private Food _summaryFood;
        public Food SummaryFood
        {
            get {
                return _summaryFood;
            }
            set
            {
                _summaryFood = value;
            }
        }

        //Initialize the SummaryFood
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
            SummaryFood = summaryfood;
            return temp;
        }
        //We need this to show the selected pizza condiments in the views
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
        //These stores the each condiments so in the end we can show the customer what he bought(delivery)
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
        /// <summary>
        /// Gives back all of the foods price without currency
        /// </summary>
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
   
        /// <summary>
        /// Add food to our pizzacondiments what shows every view at the top
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
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
                    NotifyOfPropertyChange(() => Price);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// We add here the choosen food from the view, so later we can calculate the prices and get the names etc.. 
        /// </summary>
        /// <param name="food"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool AddAt(Food food, int index) {
            if (food != null)
            {
                foods[index] = food;
                NotifyOfPropertyChange(() => Price);
                NotifyOfPropertyChange(() => PizzaCondiments);
                NotifyOfPropertyChange(() => GetPriceInCurrency);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Removes a food from the pizzaCondiments
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        public bool Remove(Food food) {
            if (food != null) {
                pizzaCondiments.Remove(food);
                NotifyOfPropertyChange(() => Price);
                return true;
            }
            return false;
        }
        /// <summary>
        /// We need the for unit tests
        /// </summary>
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
