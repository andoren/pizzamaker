using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models
{
    abstract class Food
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        protected double _price;

        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private byte[] _picture;

        public byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }


    }
}
