using pizzamaker.Models.Exceptions;
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


    }
}
