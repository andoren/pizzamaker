﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models.Foods
{
    class Topping:Food
    {
        public Topping():base()
        {
            Name = "Normal dough";
            Description = "This is a regular dough where we add all the ingridients what is needed to a good pizza dough";
            Price = 1.99;
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
