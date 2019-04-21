using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pizzamaker.Models.Logging
{
    class FileLog : BaseLogging
    {
        public string filePath = Directory.GetCurrentDirectory()+@"\Logs\Logs.txt";
        public override void Log(string what, string command, string message)
        {
            try
            {
                lock (lockObj)
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        streamWriter.WriteLine("Error: When: {0}\t ClassName: {1}\t Command: {2}\t Result: {3}", DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString(), what, command, message);
                        streamWriter.Close();
                    }
                }
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }
    }
}
