using Caliburn.Micro;
using pizzamaker.Models;
using pizzamaker.Models.Foods;
using pizzamaker.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using pizzamaker.Models.Singletons;

namespace pizzamaker.ViewModels
{
    public class ChooseCheeseViewModel : Screen
    {


        public ChooseCheeseViewModel(StartUpViewModel mainWindow)
        {
            this.mainWindow = mainWindow;
            Initialize();
            LoadOrderData();
        }
        #region Initialize data
        /// <summary>
        /// We initialize here the necessary data what is needed to the view. The Buttons commands, the foods .
        /// </summary>
        private void Initialize()
        {
            var databasehelper = DatabaseHelperProxy.getInstance();
            Cheeses = databasehelper.GetFoodsByType("cheese");
            SelectedCheeseCommand = new RelayCommand(CheeseSelected, param => this.canExecute);
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
        }
        /// <summary>
        /// If we came back to this page then load the previously selected data
        /// </summary>
        private void LoadOrderData()
        {
            Order = Order.getInstance();
            if (Order.Cheese != null)
            {
                SelectedCheese = Order.Cheese;
                Order.Remove(SelectedCheese);
            }
        }
        #endregion

        #region View properties and Methods like page changing or current Cheese
        private StartUpViewModel mainWindow;
        private Order order;

        public Order Order
        {
            get { return order; }
            set { order = value; }
        }
        //We store here the visualizeable foods
        public BindableCollection<Food> Cheeses { get; set; }
        private Cheese _selectedCheese;
        //Add the food to our order, and changes the selected food picture from the view
        public Cheese SelectedCheese
        {
            get { return _selectedCheese; }
            set
            {


                _selectedCheese = value;
                order.AddAt(SelectedCheese, 4);
                NotifyOfPropertyChange(() => SelectedCheese);
            }
        }

        //Loads the prev view by mainWindow
        public void LoadNextView()
        {
            try
            {
                if (SelectedCheese == null)
                {
                    MessageBox.Show("You must selected a Cheese first!");
                    return;
                }
                Order.Remove(_selectedCheese);
                Order.Cheese = SelectedCheese;
                Order.AddAt(SelectedCheese, 4);
                mainWindow.LoadNextView();
            }
            catch (Exception e) {
                var logger = LogHelper.getInstance();
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "LoadNextView", e.Message);
            }
        }
        //Loads the prev view by mainWindow
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
        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
        DispatcherTimer scrollTimer;
        private ICommand selectedCheeseCommand;
        //Command for the scroller image button
        public ICommand SelectedCheeseCommand
        {
            get
            {
                return selectedCheeseCommand;
            }
            set
            {
                selectedCheeseCommand = value;
            }
        }
        //The actual command of the SelectedCheeseCommand
        public void CheeseSelected(object obj)
        {
            if (obj is Cheese)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                SelectedCheese = obj as Cheese;
                order.AddAt(SelectedCheese, 4);
                NotifyOfPropertyChange(() => SelectedCheese);

            }
        }
        private ICommand scrollerToLeft;
        //Command for the scroller button
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
        //Command for the scroller button
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
                logger.Log(Models.Logging.LogType.DbLog, this.GetType().ToString(), "RemoveHandlers", e.Message);
            }

        }
        #endregion
    }
}
