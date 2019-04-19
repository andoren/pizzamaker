using pizzamaker.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models.Foods
{
    class Sauce:FoodProxy
    {
        public Sauce():base()
        {
            Name = "Normal Sauce";
            Description = "This is a regular sauce where we add all the ingridients what is needed to a good pizza sauce";
            Price = 1.99;
        }
        public Sauce(int Id, string Name, string Description, double Price):this()
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
        }
        public Sauce(int Id, string Name, string Description, double Price, byte[] RawPicture) : this(Id, Name, Description, Price)
        {
            this.RawPicture = RawPicture;
        }
    }
}
