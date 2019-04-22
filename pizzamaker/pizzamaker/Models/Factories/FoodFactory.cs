using pizzamaker.Models.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pizzamaker.Models.Exceptions;

namespace pizzamaker.Models.Factories
{
    public class FoodFactory
    {
        /// <summary>
        /// Gives back type of food. Type can be  dough,sauce,meat,topping,cheese.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public  Food GetFood(string type)
        {
            Food temp = new Dough();
            if (type == null) throw new NullReferenceException();
            
            else if (type.Length > 10 || type.Length < 4) {
                throw new InvalidLegthExceptionForFood();
            }
            else {
            
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
            }
            return temp;
        }
    }
}
