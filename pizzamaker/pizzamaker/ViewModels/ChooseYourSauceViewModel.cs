using Caliburn.Micro;
using pizzamaker.Models;
using pizzamaker.Models.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace pizzamaker.ViewModels
{
    class ChooseYourSauceViewModel:Screen
    {
        public ChooseYourSauceViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Initialize();
            LoadOrderData();

        }
        #region Initialize data
        void Initialize()
        {
            Sauces = new BindableCollection<Food>() { new Sauce(), new Sauce(1, "Spicy Sauce", "This is our Spicy sauce", 1.99) };
            SelectedSauceCommand = new RelayCommand(SauceSelected, param => this.canExecute);
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
        }
        private void LoadOrderData()
        {
            var order = Order.getInstance();
            if (order.Sauce != null) SelectedSauce = order.Sauce;
        }
        #endregion
        #region View properties and methods like current Sauce
        //
        public BindableCollection<Food> Sauces { get; set; }
        private StartUpViewModel mainWindow;
        public void SauceSelected(object obj)
        {
            if (obj is Sauce)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                SelectedSauce = obj as Sauce;
            }
        }
        private Sauce _selectedSauce;
        public Sauce SelectedSauce
        {
            get { return _selectedSauce; }
            set
            {
                _selectedSauce = value;

                NotifyOfPropertyChange(() => SelectedSauce);
            }
        }
        public void LoadNextView()
        {
            mainWindow.LoadNextView();
        }
        public void LoadPrevView()
        {
            mainWindow.LoadPrevView();
        }
        #endregion
        #region Scrollers properties and methods
        //This timer moves the scroller in the view.
        DispatcherTimer scrollTimer;

        private bool canExecute = true;
        //Command for the scroller image button
        private ICommand selectedSauceCommand;
        public ICommand SelectedSauceCommand
        {
            get
            {
                return selectedSauceCommand;
            }
            set
            {
                selectedSauceCommand = value;
            }
        }
        //Command for the scroller button
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
        //Command for the scroller button
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

        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
        /// <summary>
        /// Gets the object(Scroller) as command parameter from the view and moving the scroller right with DispatcherTimer
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Gets the object(Scroller) as command parameter from the view and moving the scroller left with DispatcherTimer
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Removes the given DispatcherTimer tick events if has any.
        /// </summary>
        /// <param name="dispatchTimer"></param>
        private void RemoveHandlers(DispatcherTimer dispatchTimer)
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
        #endregion
    }

}
