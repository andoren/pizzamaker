using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pizzamaker.Models.Logging;

namespace pizzamaker.Models.Singletons
{
    class LogHelper 
    {
        private LogHelper()
        {

        }
        private volatile static LogHelper logHelper;
        public static LogHelper getInstance() {
            if (logHelper == null) {
                lock (logHelper)
                {
                    if (logHelper == null) {
                        logHelper = new LogHelper();
                    }
                }
            }
            return logHelper;
        }
        private BaseLogging logger = null;
        public void Log(LogType logType,string what,string command, string message)
        {
            switch (logType)
            {
                case LogType.DbLog:
                    logger = new DBLog();
                    break;
                case LogType.FileLog:
                    logger = new FileLog();
                    break;
                default:
                    break;
            }
            logger.Log(what,command,message);
        }
    }
}
