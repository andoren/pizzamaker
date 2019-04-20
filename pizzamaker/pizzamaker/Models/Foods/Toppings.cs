using Caliburn.Micro;
using pizzamaker.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models.Foods
{
    public class Toppings:FoodProxy
    {
        public Toppings()
        {
            AllToppings = new List<Topping>();
        }
        private List<Topping> toppings;

        public List<Topping> AllToppings
        {
            get { return toppings; }
            set { toppings = value; }
        }

        public override double Price {
            get {
                double prices = 0;
                foreach (Food topping in AllToppings)
                {
                    prices += topping.Price;
                }
                return prices;
            }
        }
        public void AddTopping(Topping topping)
        {
            AllToppings.Add(topping);
            NotifyOfPropertyChange("AllToppings");
            NotifyOfPropertyChange("Price");
        }
        public void RemoveTopping(Topping topping)
        {
            AllToppings.Remove(topping);
            NotifyOfPropertyChange("AllToppings");
            NotifyOfPropertyChange("Price");
        }



    }
}
