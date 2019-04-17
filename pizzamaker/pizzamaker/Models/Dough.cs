﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models
{
    public class Dough:Food
    {
        public Dough()
        {
            Name = "Normal dough";
            Description = "This is a regular dough where we add all the ingridients what is needed to a good pizza dough";
            Price = 1.99;
        }
        public Dough(int Id, string Name, string Description, double Price)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
        }
        public Dough(int Id,string Name, string Description, double Price, byte[] Picture ):this(Id,Name,Description,Price)
        {
            this.Picture = Picture;
        }
    }
}
