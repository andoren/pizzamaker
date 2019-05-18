using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using pizzamaker.Models.Singletons;
using System.Diagnostics;
using pizzamaker.Models.Logging;

namespace pizzamaker.ViewModels
{
    public class StartUpViewModel:Conductor<Screen>
    {


        public StartUpViewModel()
        {
            ActivateItem(sceneFactory.CreateScene(this, 0));                    
            TextWriterTraceListener filelogger = new TextWriterTraceListener(filePath, "myListener");
            Trace.Listeners.Add(filelogger);
            log = LogHelper.getInstance();
            Trace.WriteLine("The program has started");
            Trace.Flush();

         }
        LogHelper log;
        public string filePath = System.IO.Directory.GetCurrentDirectory() + @"\Logs\Logs.txt";
        SceneFactory sceneFactory = new SceneFactory();
        private int _currentLoadedView = 0;

        public int CurrentLoadedView
        {
            get { return _currentLoadedView; }
            set
            {
                _currentLoadedView = value;
                NotifyOfPropertyChange(() => CurrentLoadedView);
            }
        }
        public void LoadNextView() {
            log.Log(LogType.FileLog,GetType().ToString(),"LoadNextView","Void enter.");
            CurrentLoadedView += 1;
            log.Log(LogType.FileLog, GetType().ToString(), "LoadNextView", $"Increased the number of the int(currentloadedview): {CurrentLoadedView}");
            ActivateItem(sceneFactory.CreateScene(this,CurrentLoadedView));
            log.Log(LogType.FileLog, GetType().ToString(), "LoadNextView", "Void leave.");
        }
        public void LoadPrevView() {
            log.Log(LogType.FileLog, GetType().ToString(), "LoadPrevView", "Void enter.");
            CurrentLoadedView -= 1;
            log.Log(LogType.FileLog, GetType().ToString(), "LoadPrevView", $"Decreased the number of the int(currentloadedview): {CurrentLoadedView}");
            ActivateItem(sceneFactory.CreateScene(this, CurrentLoadedView));
            log.Log(LogType.FileLog, GetType().ToString(), "LoadPrevView", "Void leave.");
        }

    }
}
