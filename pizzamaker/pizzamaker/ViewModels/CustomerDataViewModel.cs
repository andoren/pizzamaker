using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.ViewModels
{
    class CustomerDataViewModel:Screen
    {
        public string NameError { get; set; } = "Invalid Name";
        public string EmailError { get; set; } = "Invalid E-mail";
        public string MobilError { get; set; } = "Invalid Mobil number";
        public string AddressError { get; set; } = "Invalid Address ";
    }
}
