using pizzamaker.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models.Foods
{
    public class Topping:FoodProxy
    {
        public Topping():base()
        {

        }
        public Topping(int Id, string Name, string Description, double Price):this()
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
        }
        public Topping(int Id, string Name, string Description, double Price, byte[] RawPicture) : this(Id, Name, Description, Price)
        {
            this.RawPicture = RawPicture;
        }
    }
}
