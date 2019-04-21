using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzamaker.Models.Logging
{
    public abstract class BaseLogging
    {
        protected readonly object lockObj = new object();
        public abstract void Log(string what, string command, string message);

    }
}
