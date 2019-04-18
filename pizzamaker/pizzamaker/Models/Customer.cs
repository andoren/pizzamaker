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
        private string _mobileNumber;

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { _mobileNumber = value; }
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

        public bool EmailValidaton()
        {
            bool answer = true;
            if (String.IsNullOrEmpty(Email)) answer = false;
            else if (Email.Contains(" ")) answer = false;
            else if (!Email.Contains("@")) answer = false;
            else if (Email.Length < 7 || Email.Length > 30) answer = false;
            else if (Email.Split('@')[0].Length == 0 || Email.Split('@')[1].Length == 0) answer = false;
            else if (!Email.Contains(".")) answer = false;
            else if (Email.Split('.')[1].Length == 0) answer = false;
            return answer;
        }

        public bool MobileValidaton()
        {
            bool answer = true;
            if (String.IsNullOrEmpty(MobileNumber)) answer = false;
            else if (!MobileNumber.StartsWith("+")) answer = false;
            else if (MobileNumber.Length != 12) answer = false;
            return answer;
        }
    }
}
