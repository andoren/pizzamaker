using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pizzamaker.Models.Singletons;

namespace pizzamaker.Models.Logging
{
    class DBLog : BaseLogging
    {
        public override void Log(string what, string command, string message)
        {
            var databasehelper = DatabaseHelper.getInstance();
            databasehelper.AddLog(what, command, message);

        }
    }
}
