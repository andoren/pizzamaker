﻿using Caliburn.Micro;
using pizzamaker.Models;
using pizzamaker.Models.Foods;
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
    public class SelectToppingsViewModel:Screen
    {
        

        public SelectToppingsViewModel(StartUpViewModel mainWindow)
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
            Toppings = databasehelper.GetFoodsByType("topping");
            ScrollerToLeftCommand = new RelayCommand(ScrollerToLeft, param => this.canExecute);
            ScrollerToRightCommand = new RelayCommand(ScrollerToRight, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
            ToppingSelectedCommand = new RelayCommand(ToppingSelected, param => this.canExecute);
            ToppingRemoveCommand = new RelayCommand(RemoveTopping, param => this.canExecute);
        }
        /// <summary>
        /// If we came back to this page then load the previously selected data
        /// </summary>
        private void LoadOrderData()
        {
            Order = Order.getInstance();
            if (Order.AllToppings == null)
            {
                Order.AllToppings = new Toppings();
                
            }
            
        }
        #endregion
        #region View properties and methods like current Topping
        private StartUpViewModel mainWindow;
        //We store here the visualizeable foods
        public BindableCollection<Food> Toppings { get; set; }
        private Order order;

        public Order Order
        {
            get { return order; }
            set { order = value; }
        }
        private BindableCollection<Food> _selectedToppings;
        //Add the food to our order, and changes the selected food picture from the view
        public BindableCollection<Food> SelectedToppings
        {
            get
            {
                BindableCollection<Food> temp = new BindableCollection<Food>();
                foreach (var item in Order.AllToppings.AllToppings)
                {
                    temp.Add(item);
                }
                return temp;

            }
            set { _selectedToppings = value; }
        }

        public void LoadNextView()
        {
            try
            {
                if (Order.AllToppings.AllToppings.Count == 0)
                {
                    MessageBox.Show("You must select atleast one topping Topping first!");
                    return;
                }
                order.AddAt(Order.AllToppings, 3);
                mainWindow.LoadNextView();
            }
            catch(Exception e)
            {
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
       

        public double FullPrice {
            get
            {
                return Order.Price;
            }
        }
        #endregion
        #region Scrollers properties and methods
        //This timer moves the scroller in the view.
        DispatcherTimer scrollTimer;

        private bool canExecute = true;
        //Command for the scroller image button
        private ICommand selectedToppingCommand;
        public ICommand SelectedToppingCommand
        {
            get
            {
                return selectedToppingCommand;
            }
            set
            {
                selectedToppingCommand = value;
            }
        }
        private ICommand _toppingSelectedComand;

        public ICommand ToppingSelectedCommand
        {
            get { return _toppingSelectedComand; }
            set { _toppingSelectedComand = value; }
        }
        //Actual command of the ToppingSelectedCommand
        public void ToppingSelected(object obj)
        {
            if (obj is Topping)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }


                Order.AllToppings.AddTopping(obj as Topping);
                Order.AddAt(Order.AllToppings, 3);
                NotifyOfPropertyChange(() => FullPrice);
                NotifyOfPropertyChange(() => SelectedToppings);
            }
        }
        private ICommand _toppingRemoveCommand;
            
        public ICommand ToppingRemoveCommand
        {
            get { return _toppingRemoveCommand; }
            set { _toppingRemoveCommand = value; }
        }
        //Actual command of the ToppingRemoveCommand
        public void RemoveTopping(object obj)
        {
            if (obj is Topping)
            {
                if (scrollTimer != null)
                {
                    RemoveHandlers(scrollTimer);
                }
                Order.AllToppings.RemoveTopping(obj as Topping);
                Order.AddAt(Order.AllToppings, 3);
                if (Order.AllToppings.AllToppings.Count == 0) Order.AddAt(null, 3);
                NotifyOfPropertyChange(() => FullPrice);
                NotifyOfPropertyChange(() => SelectedToppings);
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
