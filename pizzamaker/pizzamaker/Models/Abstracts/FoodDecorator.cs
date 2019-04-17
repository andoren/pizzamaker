using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models
{
    class FoodDecorator:Food
    {
        public FoodDecorator(Food food)
        {
            this.food = food;
        }
        Food food;
        public override double Price { get {
                return food.Price + this._price;
            }
            set {
                this._price = value;
            }
        }
    }
}
