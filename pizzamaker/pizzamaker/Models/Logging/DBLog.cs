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
        /// <summary>
        /// Log into the database. First param: Which class caused the error. Second param: Which Command caused the error. Third param: The message that exception has created.
        /// </summary>
        /// <param name="what"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public override void Log(string what, string command, string message)
        {
            var databasehelper = DatabaseHelper.getInstance();
            databasehelper.AddLog(what, command, message);

        }
    }
}
