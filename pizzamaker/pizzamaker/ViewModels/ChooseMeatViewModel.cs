﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.ViewModels
{
    class ChooseMeatViewModel:Screen
    {
        private StartUpViewModel mainWindow;

        public ChooseMeatViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
        }
    }
}