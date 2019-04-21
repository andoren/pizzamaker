using pizzamaker.Models.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models.Factories
{
    class FoodFactory
    {
        public static Food GetFood(string type)
        {
            Food temp = new Dough();
            switch (type)
            {
                case "dough":
                    break;
                case "sauce":
                    temp = new Sauce();
                    break;
                case "meat":
                    temp = new Meat();
                    break;
                case "topping":
                    temp = new Topping();
                    break;
                case "cheese":
                    temp = new Cheese();
                    break;

                default:
                    break;
            }
            return temp;
        }
    }
}
