﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pizzamaker.Models.Exceptions;

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
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public bool NameValidation()
        {
            bool answer = true;
            if (String.IsNullOrEmpty(Name)) answer = false;
            else if (String.IsNullOrWhiteSpace(Name)) answer = false;
            else if (!Name.Contains(" ")) answer = false;
            else if (Name.Length < 6 || Name.Length > 30) answer = false;
            return answer;
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

        public bool AddressValidation()
        {
            bool answer = true;
            if (String.IsNullOrEmpty(Address)) answer = false;
            return answer;
        }
        /// <summary>
        /// If everything is okay its return true else throws exceptions.
        /// </summary>
        /// <returns></returns>
        public bool MassValidation()
        {
            if (!NameValidation()) throw new CustomerNameErrorException();
            else if (!EmailValidaton()) throw new CustomerEmailErrorException();
            else if (!MobileValidaton()) throw new CustomerMobileNumberErrorException();
            else if (!AddressValidation()) throw new CustomerAddressErrorException();
            return true;
        }
    }
}
