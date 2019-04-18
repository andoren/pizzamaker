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
        public string NameError { get; set; } 
        public string EmailError { get; set; } 
        public string MobilError { get; set; } 
        public string AddressError { get; set; }

    }
}
