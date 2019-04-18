using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models
{
   public class Customer
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _mobilNumber;

        public string MobilNumber
        {
            get { return _mobilNumber; }
            set { _mobilNumber = value; }
        }
        private string _address;

        public bool NameValidation()
        {
            bool answer = true;
            if (String.IsNullOrEmpty(Name)) answer = false;
            else if (String.IsNullOrWhiteSpace(Name)) answer = false;
            else if (!Name.Contains(" ")) answer = false;
            else if (Name.Length < 6 || Name.Length > 30) answer = false;
            return answer;
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }





    }
}
