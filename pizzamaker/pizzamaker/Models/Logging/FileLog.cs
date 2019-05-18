using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace pizzamaker.Models.Logging
{
    class FileLog : BaseLogging
    {
        
        /// <summary>
        /// Log into the database. First param: Which class caused the error. Second param: Which Command caused the error. Third param: The message that exception has created.
        /// </summary>
        /// <param name="what"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public override void Log(string what, string command, string message)
        {
            try
            {
                lock (lockObj)
                {

                        Trace.WriteLine(string.Format("When: {0}\t ClassName: {1}\t Command: {2}\t Result: {3}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), what, command, message));
                        Trace.Flush();

                }
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }
    }
}
