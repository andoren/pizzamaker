﻿using pizzamaker.Models.Exceptions;
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
            pizzaCondiments = new List<Food>();
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
        private List<Food> pizzaCondiments;
        private Customer customer;
        public Dough dough;
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
            pizzaCondiments = new List<Food>();
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
