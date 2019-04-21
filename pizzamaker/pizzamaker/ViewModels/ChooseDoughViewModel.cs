
using Caliburn.Micro;
using pizzamaker.Models;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Threading;
using System.Reflection;
using pizzamaker.Models.Singletons;

namespace pizzamaker.ViewModels
{
    public class ChooseDoughViewModel : Screen
    {

        public ChooseDoughViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Initialize();
            LoadOrderData();
        }
        #region Initialize data
        private void Initialize()
        {
            var dataaccess = DatabaseHelper.getInstance();
            Doughs = dataaccess.GetFoodsByType("dough");
            SelectedDoughCommand = new RelayCommand(DoughSelected, param => this.canExecute);
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
            

        }
        private void LoadOrderData()
        {
            Order= Order.getInstance();
            if (Order.Dough != null) {
                SelectedDough = Order.Dough;
                Order.Remove(SelectedDough);
            } 
        }
        #endregion

        #region View properties and Methods like page changing or current dough
        private StartUpViewModel mainWindow;
        private Order order ;

        public Order  Order
        {
            get { return order; }
            set { order = value; }
        }

        public BindableCollection<Food> Doughs { get; set; }
        private Dough _selectedDough;
        public Dough SelectedDough
        {
            get { return _selectedDough; }
            set {
                
                
                _selectedDough = value;
                order.AddAt(SelectedDough, 0);
                NotifyOfPropertyChange(() => SelectedDough);
            }
        }

        public void DoughSelected(object obj) {
            if (obj is Dough) {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                SelectedDough = obj as Dough;
            }
        }
        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
        public void LoadNextView()
        {
            try
            {
                if (SelectedDough == null)
                {
                    MessageBox.Show("You must selected a dough first!");
                    return;
                }
                Order.Remove(_selectedDough);
                Order.Dough = SelectedDough;
                Order.AddAt(SelectedDough, 0);
                mainWindow.LoadNextView();
            }
            catch (Exception e) {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadNextView", e.Message);
            }
        }
        public void LoadPrevView()
        {
            try
            {
                mainWindow.LoadPrevView();
            }
            catch (Exception e) {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadPrevView", e.Message);
            }
        }
        #endregion
        #region Scrollers properties and methods
        DispatcherTimer scrollTimer;
        private ICommand selectedDoughCommand;
        public ICommand SelectedDoughCommand
        {
            get
            {
                return selectedDoughCommand;
            }
            set
            {
                selectedDoughCommand = value;
            }
        }
        private ICommand scrollerToLeft;
        public ICommand ScrollerToLeftCommand
        {
            get
            {
                return scrollerToLeft;
            }
            set
            {
                scrollerToLeft = value;
            }
        }
        private ICommand scrollerToRight;
        public ICommand ScrollerToRightCommand
        {
            get
            {
                return scrollerToRight;
            }
            set
            {
                scrollerToRight = value;
            }
        }
        private ICommand toggleExecuteCommand { get; set; }
        public ICommand ToggleExecuteCommand
        {
            get
            {
                return toggleExecuteCommand;
            }
            set
            {
                toggleExecuteCommand = value;
            }
        }
        private bool canExecute = true;
        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }
        public void ScrollerToRight(object obj)
        {
            if (obj is ScrollViewer)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                ScrollViewer scrollViewer = obj as ScrollViewer;
                scrollTimer = new DispatcherTimer();

                scrollTimer.Start();
                double curroffset = scrollViewer.HorizontalOffset;


                scrollTimer.Interval = TimeSpan.FromTicks(5000);

                scrollTimer.Tick += (s, e) =>
                {
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 0.2);

                    if (curroffset + 170 <= scrollViewer.HorizontalOffset)
                    {
                        scrollTimer.Stop();
                    }
                };

            }
        }
        public void ScrollerToLeft(object obj)
        {
            if (obj is ScrollViewer)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                ScrollViewer scrollViewer = obj as ScrollViewer;
                scrollTimer = new DispatcherTimer();

                scrollTimer.Start();
                double curroffset = scrollViewer.HorizontalOffset;
                scrollTimer.Interval = TimeSpan.FromTicks(5000);

                scrollTimer.Tick += (s, e) =>
                {
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - 0.2);

                    if (curroffset - 170 >= scrollViewer.HorizontalOffset)
                    {
                        scrollTimer.Stop();
                    }
                };
            }
        }
        private void RemoveHandlers(DispatcherTimer dispatchTimer)
        {
            try
            {
                var eventField = dispatchTimer.GetType().GetField("Tick",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                var eventDelegate = (Delegate)eventField.GetValue(dispatchTimer);
                if (eventDelegate != null)
                {
                    var invocatationList = eventDelegate.GetInvocationList();

                    foreach (var handler in invocatationList)
                        dispatchTimer.Tick -= ((EventHandler)handler);
                }
            }
            catch (Exception e) {

                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "RemoveHandler", e.Message);
            }

        }
        #endregion
    }
}