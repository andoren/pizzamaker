using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models
{
    class Order:Food
    {
        private Order()
        {
            pizzaCondiments = new List<Food>();
        }
        static Order order = null;

        public static Order getInstance() {
            if (order == null) {
                lock (order) {
                    if (order == null) order = new Order();
                }
                
            }
            return order;
        }
        private List<Food> pizzaCondiments;

    }
}
