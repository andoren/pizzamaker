﻿using System;
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
                lock (typeof(LogHelper))
                {
                    if (logHelper == null) {
                        logHelper = new LogHelper();
                    }
                }
            }
            return logHelper;
        }
        private BaseLogging logger = null;
        /// <summary>
        /// Through this we use the logging system. We can choose which type of logger do we want to use.
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="what">Which class caused the error</param>
        /// <param name="command">Which command caused the error</param>
        /// <param name="message">The meassage of the exception</param>
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
